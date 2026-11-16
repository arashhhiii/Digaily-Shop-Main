
(function(){
  var $=function(id){return document.getElementById(id);};

  /* ------------------------------------------------------------
     Settings
     sendCode / checkCode are DEMO stand-ins. Replace their bodies
     with calls to your SMS backend (for example Twilio Verify) and
     set DEMO to false to hide the on-screen code.
     ------------------------------------------------------------ */
  var DEMO=true;
  var RESEND_SECONDS=120;      // resend link appears after two minutes
  var MAX_TRIES=5;             // wrong guesses allowed per code
  var MAIN='/';
  var KEY='digaily-verify';

  function sendCode(phone){            // phone looks like "+989123456789"
    return new Promise(function(resolve){
      setTimeout(function(){resolve(String(Math.floor(100000+Math.random()*900000)));},700);
    });
  }
  function checkCode(phone,code){      // resolves true when the code is right
    return new Promise(function(resolve){
      setTimeout(function(){resolve(code===state.demoCode);},600);
    });
  }

  /* what the sign in page hands over (sessionStorage 'digaily-verify'):
     {phone:'+989123456789', sentAt:<ms>, demoCode:'123456'}            */
  var state=null;
  try{state=JSON.parse(sessionStorage.getItem(KEY)||'null');}catch(e){}
  var q=new URLSearchParams(location.search);
  if(!state||!state.phone){
    state={phone:q.get('phone')||'+989123456789',sentAt:Date.now(),demoCode:String(Math.floor(100000+Math.random()*900000))};
  }
  if(!state.sentAt)state.sentAt=Date.now();
  if(!state.demoCode)state.demoCode=String(Math.floor(100000+Math.random()*900000));
  function save(){try{sessionStorage.setItem(KEY,JSON.stringify(state));}catch(e){}}
  save();

  function mask(p){                    // "+989123456789" -> "+98 •••••••789" (assumes a 10 digit national number)
    var d=p.replace(/\D/g,''), cc=Math.max(1,d.length-10), rest=d.slice(cc);
    return '+'+d.slice(0,cc)+' '+new Array(Math.max(0,rest.length-3)+1).join('•')+rest.slice(-3);
  }
  $('codeTo').textContent=state.shown||mask(state.phone);

  var otp=$('otp'), boxes=[].slice.call(otp.querySelectorAll('input'));
  var resendBtn=$('resend'), resendTxt=$('resendTxt'), resendBox=$('resendBox'), bar=$('bar');
  var tries=0, timer=null;

  function showDemo(){
    $('demoHint').hidden=!DEMO;
    if(DEMO)$('demoCode').textContent=state.demoCode;
  }
  showDemo();

  /* two minute resend countdown (based on the clock, so it stays right in background tabs
     and survives a page reload) */
  function pad(n){return n<10?'0'+n:''+n;}
  function tick(){
    var left=Math.max(0,Math.ceil((state.sentAt+RESEND_SECONDS*1000-Date.now())/1000));
    if(left>0){
      resendTxt.textContent='Resend code in '+Math.floor(left/60)+':'+pad(left%60);
      resendBtn.hidden=true;
      resendBox.classList.remove('ready');
      bar.style.transform='scaleX('+(left/RESEND_SECONDS)+')';
    }else{
      stopTimer();
      resendTxt.textContent='Did not get the code?';
      resendBtn.hidden=false;
      resendBox.classList.add('ready');
      bar.style.transform='scaleX(1)';
      $('live').textContent='You can request a new code now.';
    }
  }
  function startTimer(){stopTimer();tick();timer=setInterval(tick,500);}
  function stopTimer(){if(timer){clearInterval(timer);timer=null;}}
  startTimer();

  function say(text,kind){var m=$('mCode');m.textContent=text||'';m.className='msg'+(kind?' '+kind:'');}
  function clearBoxes(){
    boxes.forEach(function(b){b.value='';b.removeAttribute('aria-invalid');b.disabled=false;});
    $('otpE').textContent='';
  }
  function clearInvalid(){
    boxes.forEach(function(b){b.removeAttribute('aria-invalid');});
    $('otpE').textContent='';say('');
  }

  resendBtn.addEventListener('click',function(){
    resendBtn.disabled=true;say('');
    sendCode(state.phone).then(function(code){
      resendBtn.disabled=false;
      state.demoCode=code;state.sentAt=Date.now();save();
      clearBoxes();tries=0;
      $('bCode').disabled=false;
      showDemo();
      say('A new code is on its way.','good');
      startTimer();
      boxes[0].focus();
    },function(){
      resendBtn.disabled=false;
      say('Could not send a new code. Try again.','bad');
    });
  });

  /* the six boxes */
  function codeValue(){return boxes.map(function(b){return b.value;}).join('');}
  function fillFrom(start,str){
    for(var j=0;j<str.length&&start+j<boxes.length;j++)boxes[start+j].value=str.charAt(j);
    boxes[Math.min(start+str.length,boxes.length-1)].focus();
  }
  boxes.forEach(function(b,i){
    b.addEventListener('input',function(){
      var v=b.value.replace(/\D/g,'');
      clearInvalid();
      if(v.length>=boxes.length){fillFrom(0,v.slice(0,boxes.length));return;}   // autofill from the SMS
      b.value=v.slice(-1);
      if(b.value&&i<boxes.length-1)boxes[i+1].focus();
    });
    b.addEventListener('keydown',function(e){
      if(e.key==='Backspace'&&!b.value&&i>0){e.preventDefault();boxes[i-1].value='';boxes[i-1].focus();}
      else if(e.key==='ArrowLeft'&&i>0){e.preventDefault();boxes[i-1].focus();}
      else if(e.key==='ArrowRight'&&i<boxes.length-1){e.preventDefault();boxes[i+1].focus();}
    });
    b.addEventListener('paste',function(e){
      e.preventDefault();
      var t=((e.clipboardData||window.clipboardData).getData('text')||'').replace(/\D/g,'');
      if(!t)return;
      clearInvalid();
      fillFrom(t.length>=boxes.length?0:i,t.slice(0,boxes.length));
    });
    b.addEventListener('focus',function(){b.select();});
  });

  $('fCode').addEventListener('submit',function(e){
    e.preventDefault();
    var code=codeValue();
    if(code.length<boxes.length){
      boxes.forEach(function(b){if(!b.value)b.setAttribute('aria-invalid','true');});
      $('otpE').textContent='Enter all 6 digits.';
      (boxes.filter(function(b){return !b.value;})[0]||boxes[0]).focus();
      return;
    }
    var btn=$('bCode');btn.disabled=true;btn.textContent='Checking…';
    checkCode(state.phone,code).then(function(ok){
      if(ok){
        stopTimer();
        try{localStorage.setItem('digaily-user',state.phone);sessionStorage.removeItem(KEY);}catch(e){}
        say('Verified! Taking you to the shop…','good');
        btn.textContent='One moment…';
        setTimeout(function(){location.href=MAIN;},1300);
        return;
      }
      tries++;
      boxes.forEach(function(b){b.setAttribute('aria-invalid','true');});
      otp.classList.remove('shake');void otp.offsetWidth;otp.classList.add('shake');
      if(tries>=MAX_TRIES){
        boxes.forEach(function(b){b.disabled=true;});
        say('Too many wrong tries. Ask for a new code.','bad');
        btn.textContent='Verify and sign in';           // stays disabled until a new code is sent
        stopTimer();                                    // let them ask for a new code right away
        resendTxt.textContent='Did not get the code?';
        resendBtn.hidden=false;resendBox.classList.add('ready');bar.style.transform='scaleX(1)';
      }else{
        btn.disabled=false;btn.textContent='Verify and sign in';
        var left=MAX_TRIES-tries;
        say('That code is not right. '+left+(left===1?' try':' tries')+' left.','bad');
        boxes.forEach(function(b){b.value='';});
        boxes[0].focus();
      }
    },function(){
      btn.disabled=false;btn.textContent='Verify and sign in';
      say('Could not check the code. Please try again.','bad');
    });
  });

  boxes[0].focus({preventScroll:true});
})();