
(function(){
  var ICON={
    mail:'<rect x="3" y="5" width="18" height="14" rx="3"/><path d="M4 7l8 6 8-6"/>',
    phone:'<rect x="7" y="2.5" width="10" height="19" rx="2.6"/><path d="M10.5 18.5h3"/>',
    lock:'<rect x="5" y="11" width="14" height="9" rx="3"/><path d="M8 11V8a4 4 0 018 0v3"/>',
    user:'<circle cx="12" cy="8" r="4"/><path d="M4 21a8 8 0 0116 0"/>',
    gift:'<rect x="3" y="8" width="18" height="4" rx="1"/><path d="M12 8v13"/><path d="M5 12v7a2 2 0 002 2h10a2 2 0 002-2v-7"/><path d="M12 8C10 4 6 4.5 7 7c.5 1.2 3 1 5 1zM12 8c2-4 6-3.5 5-1-.5 1.2-3 1-5 1z"/>',
    check:'<path d="M5 12.5l4.5 4.5L19 7.5"/>',
    arrow:'<path d="M5 12h14M13 6l6 6-6 6"/>',
    left:'<path d="M19 12H5M11 6l-6 6 6 6"/>',
    eye:'<path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12z"/><circle cx="12" cy="12" r="3"/>',
    eyeoff:'<path d="M3 3l18 18"/><path d="M10.6 5.1A10.5 10.5 0 0112 5c6.5 0 10 7 10 7a17 17 0 01-3.2 4.2M6.6 6.6A17 17 0 002 12s3.5 7 10 7c1.7 0 3.2-.4 4.5-1"/><path d="M9.9 9.9a3 3 0 004.2 4.2"/>'
  };
  function svg(n,s,stroke,sw){return '<svg width="'+s+'" height="'+s+'" viewBox="0 0 24 24" fill="none" stroke="'+stroke+'" stroke-width="'+sw+'" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">'+ICON[n]+'</svg>';}
  document.querySelectorAll('[data-icon]').forEach(function(el){
    el.style.display='inline-flex';
    el.innerHTML=svg(el.dataset.icon,+el.dataset.size||20,el.dataset.stroke||'currentColor',el.dataset.sw||1.9);
  });
  var $=function(id){return document.getElementById(id);};
  var auth=$('auth'),paneIn=$('paneIn'),paneUp=$('paneUp'),paneCode=$('paneCode'),ovL=$('ovL'),ovR=$('ovR');
  var MAIN='/';

  /* show / hide password */
  document.querySelectorAll('[data-eye]').forEach(function(b){
    b.innerHTML=svg('eye',20,'#34476B',1.9);
    b.addEventListener('click',function(){
      var i=$(b.dataset.eye),show=i.type==='password';
      i.type=show?'text':'password';
      b.innerHTML=svg(show?'eyeoff':'eye',20,'#34476B',1.9);
      b.setAttribute('aria-label',show?'Hide password':'Show password');
      b.setAttribute('aria-pressed',show?'true':'false');
    });
  });

  /* slide between the forms */
  function setMode(mode,focus){
    var up=mode==='up';
    stopTimer();
    auth.classList.remove('is-code');
    auth.classList.toggle('is-up',up);
    paneIn.inert=up;paneUp.inert=!up;paneCode.inert=true;
    ovR.inert=up;ovL.inert=!up;
    ovR.setAttribute('aria-hidden',up?'true':'false');ovL.setAttribute('aria-hidden',up?'false':'true');
    document.title=(up?'Sign up':'Sign in')+' | Digaily';
    $('live').textContent=up?'Sign up form':'Sign in form';
    try{history.replaceState(null,'',up?'#signup':'#signin');}catch(e){}
    if(focus){setTimeout(function(){var f=(up?paneUp:paneIn).querySelector('input:not([type=hidden])');if(f&&!f.closest('[hidden]'))f.focus({preventScroll:true});},520);}
  }
  document.addEventListener('click',function(e){
    var b=e.target.closest('[data-go]');
    if(b){e.preventDefault();setMode(b.dataset.go,true);}
  });
  window.addEventListener('hashchange',function(){setMode(location.hash==='#signup'?'up':'in',false);});
  setMode(location.hash==='#signup'?'up':'in',false);

  /* validation helpers */
  var EMAIL=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  function fieldErr(input,errId,text){
    var e=$(errId);e.textContent=text||'';
    if(text)input.setAttribute('aria-invalid','true');else input.removeAttribute('aria-invalid');
    return !text;
  }
  function firstBad(form){return form.querySelector('[aria-invalid="true"]');}
  function say(id,text,kind){var m=$(id);m.textContent=text||'';m.className='msg'+(kind?' '+kind:'');}
  function finish(btnId,msgId,text,who){
    try{localStorage.setItem('digaily-user',who);}catch(e){}
    say(msgId,text,'good');
    var b=$(btnId);b.disabled=true;b.textContent='One moment\u2026';
    setTimeout(function(){location.href=MAIN;},1300);
  }

  /* email / phone tabs */
  var fIn=$('fIn'),fPh=$('fPh'),tabEmail=$('tabEmail'),tabPhone=$('tabPhone');
  function setMethod(m,focus){
    var ph=m==='phone';
    fIn.hidden=ph;fPh.hidden=!ph;
    tabEmail.setAttribute('aria-selected',ph?'false':'true');tabEmail.tabIndex=ph?-1:0;
    tabPhone.setAttribute('aria-selected',ph?'true':'false');tabPhone.tabIndex=ph?0:-1;
    if(focus)(ph?$('phNum'):$('inEmail')).focus({preventScroll:true});
  }
  [tabEmail,tabPhone].forEach(function(t){
    t.addEventListener('click',function(){setMethod(t.dataset.method,false);});
    t.addEventListener('keydown',function(e){
      if(e.key==='ArrowLeft'||e.key==='ArrowRight'){
        e.preventDefault();
        var next=t===tabEmail?tabPhone:tabEmail;
        setMethod(next.dataset.method,false);next.focus();
      }
    });
  });

  /* sign in with email */
  ['inEmail','inPass'].forEach(function(id){$(id).addEventListener('input',function(){this.removeAttribute('aria-invalid');$(id+'E').textContent='';say('mIn','');});});
  fIn.addEventListener('submit',function(e){
    e.preventDefault();
    var em=$('inEmail').value.trim(),pw=$('inPass').value;
    var a=fieldErr($('inEmail'),'inEmailE',!em?'Enter your email.':!EMAIL.test(em)?'That email does not look right.':'');
    var b=fieldErr($('inPass'),'inPassE',!pw?'Enter your password.':'');
    if(!(a&&b)){firstBad(fIn).focus();return;}
    finish('bIn','mIn','Welcome back! Taking you to the shop\u2026',em);
  });
  $('forgot').addEventListener('click',function(e){
    e.preventDefault();
    var em=$('inEmail').value.trim();
    if(!EMAIL.test(em)){fieldErr($('inEmail'),'inEmailE','Enter your email first and we will send a reset link.');$('inEmail').focus();return;}
    say('mIn','Reset link sent to '+em+' (demo).','good');
  });

  /* ------------------------------------------------------------
     Phone sign in
     sendCode / checkCode below are DEMO stand-ins. Replace their
     bodies with calls to your SMS backend (for example Twilio
     Verify) and set DEMO to false to hide the on-screen code.
     ------------------------------------------------------------ */
  var DEMO=true;
  var RESEND_SECONDS=60;     // a new code can be requested once per minute
  var MAX_TRIES=5;           // wrong guesses allowed per code
  var demoCode='';

  function sendCode(phone){            // phone looks like "+989123456789"
    return new Promise(function(resolve){
      setTimeout(function(){
        demoCode=String(Math.floor(100000+Math.random()*900000));
        resolve();
      },700);
    });
  }
  function checkCode(phone,code){      // resolves true when the code is right
    return new Promise(function(resolve){
      setTimeout(function(){resolve(code===demoCode);},600);
    });
  }

  var pending=null;     // {e164, shown}
  var tries=0, resendAt=0, timer=null;
  var otp=$('otp'), boxes=[].slice.call(otp.querySelectorAll('input'));
  var resendBtn=$('resend'), resendTxt=$('resendTxt');

  function dialCodes(){return [].map.call($('phCc').options,function(o){return o.value;}).sort(function(a,b){return b.length-a.length;});}

  ['phNum'].forEach(function(id){$(id).addEventListener('input',function(){this.removeAttribute('aria-invalid');$('phNumE').textContent='';say('mPh','');});});

  fPh.addEventListener('submit',function(e){
    e.preventDefault();
    var raw=$('phNum').value.trim(), digits=raw.replace(/\D/g,''), cc=$('phCc').value, err='';
    if(/^(\+|00)/.test(raw)){                       // typed with country code, like +98 912...
      if(/^00/.test(digits))digits=digits.slice(2);
      var hit=dialCodes().filter(function(d){return digits.indexOf(d)===0;})[0];
      if(hit){cc=hit;digits=digits.slice(hit.length);$('phCc').value=hit;}
      else err='Pick your country code from the list.';
    }
    digits=digits.replace(/^0+/,'');                // drop the leading 0 of national format
    if(!err){
      if(!digits)err='Enter your phone number.';
      else if(digits.length<6||digits.length>14)err='That phone number does not look right.';
    }
    if(!fieldErr($('phNum'),'phNumE',err)){$('phNum').focus();return;}
    pending={e164:'+'+cc+digits,shown:'+'+cc+' '+new Array(Math.max(0,digits.length-3)+1).join('\u2022')+digits.slice(-3)};
    var b=$('bPh');b.disabled=true;b.textContent='Sending\u2026';say('mPh','');
    sendCode(pending.e164).then(function(){
      b.disabled=false;b.textContent='Send code';
      openCode();
    },function(){
      b.disabled=false;b.textContent='Send code';
      say('mPh','We could not send the code. Please try again.','bad');
    });
  });

  /* code page */
  function clearBoxes(){
    boxes.forEach(function(b){b.value='';b.removeAttribute('aria-invalid');b.disabled=false;});
    $('otpE').textContent='';
  }
  function showDemo(){
    $('demoHint').hidden=!DEMO;
    if(DEMO)$('demoCode').textContent=demoCode;
  }
  function openCode(){
    clearBoxes();say('mCode','');tries=0;
    $('codeTo').textContent=pending.shown;
    $('bCode').disabled=false;$('bCode').textContent='Verify and sign in';
    showDemo();
    paneIn.inert=true;paneCode.inert=false;
    auth.classList.add('is-code');
    document.title='Enter code | Digaily';
    $('live').textContent='Code page. We sent a 6 digit code to your phone.';
    startTimer();
    setTimeout(function(){boxes[0].focus({preventScroll:true});},560);
  }
  function backToPhone(){
    stopTimer();
    auth.classList.remove('is-code');
    paneCode.inert=true;paneIn.inert=false;
    document.title='Sign in | Digaily';
    setMethod('phone',false);
    setTimeout(function(){$('phNum').focus({preventScroll:true});},560);
  }
  $('changeNum').addEventListener('click',backToPhone);

  /* one minute resend countdown (uses the clock, so it stays right in background tabs) */
  function pad(n){return n<10?'0'+n:''+n;}
  function tick(){
    var left=Math.max(0,Math.ceil((resendAt-Date.now())/1000));
    if(left>0){
      resendTxt.textContent='Resend code in '+Math.floor(left/60)+':'+pad(left%60);
      resendBtn.hidden=true;
    }else{
      stopTimer();
      resendTxt.textContent='Did not get it?';
      resendBtn.hidden=false;
      $('live').textContent='You can request a new code now.';
    }
  }
  function startTimer(){
    stopTimer();
    resendAt=Date.now()+RESEND_SECONDS*1000;
    resendBtn.hidden=true;
    tick();
    timer=setInterval(tick,500);
  }
  function stopTimer(){if(timer){clearInterval(timer);timer=null;}}

  resendBtn.addEventListener('click',function(){
    if(!pending)return;
    resendBtn.disabled=true;say('mCode','');
    sendCode(pending.e164).then(function(){
      resendBtn.disabled=false;
      clearBoxes();tries=0;
      $('bCode').disabled=false;
      showDemo();
      say('mCode','A new code is on its way.','good');
      startTimer();
      boxes[0].focus();
    },function(){
      resendBtn.disabled=false;
      say('mCode','Could not send a new code. Try again.','bad');
    });
  });

  /* the six boxes */
  function codeValue(){return boxes.map(function(b){return b.value;}).join('');}
  function fillFrom(start,str){
    for(var j=0;j<str.length&&start+j<boxes.length;j++)boxes[start+j].value=str.charAt(j);
    boxes[Math.min(start+str.length,boxes.length-1)].focus();
  }
  function clearInvalid(){
    boxes.forEach(function(b){b.removeAttribute('aria-invalid');});
    $('otpE').textContent='';say('mCode','');
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
    var btn=$('bCode');btn.disabled=true;btn.textContent='Checking\u2026';
    checkCode(pending.e164,code).then(function(ok){
      if(ok){
        stopTimer();
        finish('bCode','mCode','Verified! Taking you to the shop\u2026',pending.e164);
        return;
      }
      tries++;
      boxes.forEach(function(b){b.setAttribute('aria-invalid','true');});
      otp.classList.remove('shake');void otp.offsetWidth;otp.classList.add('shake');
      if(tries>=MAX_TRIES){
        boxes.forEach(function(b){b.disabled=true;});
        say('mCode','Too many wrong tries. Ask for a new code.','bad');
        btn.textContent='Verify and sign in';        // stays disabled until a new code is sent
        if(resendBtn.hidden){resendAt=Math.min(resendAt,Date.now());tick();}
      }else{
        btn.disabled=false;btn.textContent='Verify and sign in';
        say('mCode','That code is not right. '+(MAX_TRIES-tries)+(MAX_TRIES-tries===1?' try':' tries')+' left.','bad');
        boxes.forEach(function(b){b.value='';});
        boxes[0].focus();
      }
    },function(){
      btn.disabled=false;btn.textContent='Verify and sign in';
      say('mCode','Could not check the code. Please try again.','bad');
    });
  });

  /* password strength */
  function score(p){
    var s=0;
    if(p.length>=8)s++;
    if(/[a-z]/.test(p)&&/[A-Z]/.test(p))s++;
    if(/\d/.test(p))s++;
    if(/[^A-Za-z0-9]/.test(p)&&p.length>=8)s++;
    return p.length?Math.max(1,s):0;
  }
  var words=['','Weak','Fair','Good','Strong'];
  $('upPass').addEventListener('input',function(){
    var s=score(this.value);
    $('bars').dataset.s=s;
    $('strengthTxt').textContent=words[s]||'\u00a0';
    this.removeAttribute('aria-invalid');$('upPassE').textContent='';
  });
  ['upName','upEmail'].forEach(function(id){$(id).addEventListener('input',function(){this.removeAttribute('aria-invalid');$(id+'E').textContent='';});});
  $('terms').addEventListener('change',function(){$('termsE').textContent='';});

  /* sign up */
  var fUp=$('fUp');
  fUp.addEventListener('submit',function(e){
    e.preventDefault();
    var nm=$('upName').value.trim(),em=$('upEmail').value.trim(),pw=$('upPass').value;
    var ok=[
      fieldErr($('upName'),'upNameE',!nm?'Tell us your name.':''),
      fieldErr($('upEmail'),'upEmailE',!em?'Enter your email.':!EMAIL.test(em)?'That email does not look right.':''),
      fieldErr($('upPass'),'upPassE',pw.length<8?'Use at least 8 characters.':''),
    ];
    var t=$('terms').checked;$('termsE').textContent=t?'':'Please accept the terms to continue.';
    if(ok.indexOf(false)>-1||!t){(firstBad(fUp)||$('terms')).focus();return;}
    finish('bUp','mUp','Account created! Welcome, '+nm.split(' ')[0]+'. Taking you to the shop\u2026',em);
  });
})();