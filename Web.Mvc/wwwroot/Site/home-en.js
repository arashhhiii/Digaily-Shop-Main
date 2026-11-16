
(function(){
  var P=[
    {id:1,name:'Technolife Nova Smartphone',cat:'Technolife',sub:'Phones',type:'Smartphones',meta:'256 GB storage, 8 GB RAM',price:699.99},
    {id:2,name:'Wireless Gaming Headset',cat:'Gaming',sub:'Accessories',type:'Headsets',meta:'Wireless surround sound',price:89.99},
    {id:3,name:'Digikala Cotton T-Shirt',cat:'Digikala',sub:'Clothing',type:'T-Shirts',meta:'Soft cotton, multiple sizes',price:19.99},
    {id:4,name:'Espresso Coffee Beans',cat:'Food',sub:'Pantry',type:'Coffee',meta:'1 kg, freshly roasted',price:24.5},
    {id:5,name:'Bluetooth Earbuds',cat:'Electronics',sub:'Audio',type:'Headphones',meta:'Noise reduction, charging case',price:59.99},
    {id:6,name:'Wireless Game Controller',cat:'Gaming',sub:'Accessories',type:'Controllers',meta:'Compatible with PC and console',price:49.99},
    {id:7,name:'Technolife Smartwatch',cat:'Technolife',sub:'Wearables',type:'Smartwatches',meta:'Fitness tracking, 5-day battery',price:129.99},
    {id:8,name:'Everyday Zip Hoodie',cat:'Clothing',sub:'Tops',type:'Hoodies',meta:'Fleece lining, multiple sizes',price:44.99},
    {id:9,name:'4K Computer Monitor',cat:'Electronics',sub:'Computers',type:'Monitors',meta:'27-inch UHD display',price:329.99},
    {id:10,name:'Lightweight Running Sneakers',cat:'Clothing',sub:'Shoes',type:'Sneakers',meta:'Breathable mesh, cushioned sole',price:74.99},
    {id:11,name:'Dark Chocolate Selection',cat:'Food',sub:'Snacks',type:'Chocolate',meta:'Assorted 70% cocoa bars',price:14.99},
    {id:12,name:'Digikala Everyday Backpack',cat:'Digikala',sub:'Accessories',type:'Bags',meta:'Water-resistant, 20 L capacity',price:39.99},
    {id:13,name:'Starlight Racing Game',cat:'Gaming',sub:'Games',type:'Racing',meta:'Console edition',price:39.99},
    {id:14,name:'Extra-Virgin Olive Oil',cat:'Food',sub:'Pantry',type:'Olive Oil',meta:'Cold-pressed, 750 ml',price:18.99}
  ];
  var INFO={
    1:['A fast everyday smartphone with plenty of space for photos, apps and entertainment.',['256 GB storage and 8 GB RAM','Sharp high-resolution display','All-day battery life','Protective case included']],
    2:['Enjoy clear game audio and chat with a comfortable wireless headset built for long sessions.',['Surround-sound audio','Detachable microphone','Comfortable padded ear cups','Works with PC and consoles']],
    3:['A soft, everyday cotton T-shirt available in a range of sizes and colors.',['Breathable cotton fabric','Comfortable regular fit','Multiple sizes available','Machine washable']],
    4:['Freshly roasted espresso beans with a rich aroma and a smooth finish.',['Whole-bean coffee','Medium-dark roast','Packed fresh in a resealable bag','Suitable for espresso machines']],
    5:['Compact wireless earbuds for music, calls and listening on the go.',['Noise-reduction microphones','Portable charging case','Touch controls','Bluetooth connection']],
    6:['A responsive wireless controller for gaming on PC or console.',['Comfortable ergonomic grip','Responsive buttons and triggers','Wireless connection','Compatible with PC and console']],
    7:['A versatile smartwatch for tracking daily activity and staying connected.',['Heart-rate and activity tracking','Notifications on your wrist','Up to 5-day battery life','Water-resistant design']],
    8:['A warm zip-up hoodie designed for comfortable everyday wear.',['Soft fleece lining','Full-length front zipper','Adjustable hood','Multiple sizes available']],
    9:['A crisp 4K monitor for work, streaming and PC gaming.',['27-inch UHD display','Vivid color and sharp details','Adjustable tilt stand','HDMI and DisplayPort inputs']],
    10:['Lightweight sneakers with breathable fabric and cushioning for daily walks or runs.',['Breathable mesh upper','Cushioned insole','Durable rubber outsole','Multiple sizes available']],
    11:['A selection of rich dark chocolate bars for sharing or enjoying as a treat.',['70% cocoa chocolate','Assorted flavors','Individually wrapped bars','Made with cocoa butter']],
    12:['A practical backpack with room for daily essentials, work or school.',['20-liter capacity','Water-resistant outer fabric','Padded laptop compartment','Adjustable shoulder straps']],
    13:['Race through colorful tracks and compete in fast-paced console challenges.',['Console game edition','Single-player and multiplayer modes','Unlockable cars and tracks','Suitable for all skill levels']],
    14:['Cold-pressed extra-virgin olive oil for cooking, dressing and dipping.',['750 ml bottle','Cold-pressed olives','Rich, fruity flavor','Store in a cool, dry place']]
  };
  var TONE={'Technolife':['#1E4FB8','#FFFBE6'],'Digikala':['#BCD4F7','#0F2547'],'Electronics':['#2F6FD0','#FFFBE6'],'Clothing':['#E8B4C8','#0F2547'],'Gaming':['#FFD84D','#0F2547'],'Food':['#D6E8B8','#0F2547']};
  var ICON={
    shield:'<path d="M12 3l7 3v5c0 4.5-3 8-7 10-4-2-7-5.5-7-10V6l7-3z"/><path d="M9 12l2 2 4-4"/>',
    game:'<rect x="3" y="7" width="18" height="11" rx="4"/><path d="M8 10.5v4M6 12.5h4"/><circle cx="15.5" cy="11.5" r="0.8"/><circle cx="17.5" cy="13.5" r="0.8"/>',
    gift:'<rect x="3" y="8" width="18" height="4" rx="1"/><path d="M12 8v13"/><path d="M5 12v7a2 2 0 002 2h10a2 2 0 002-2v-7"/><path d="M12 8C10 4 6 4.5 7 7c.5 1.2 3 1 5 1zM12 8c2-4 6-3.5 5-1-.5 1.2-3 1-5 1z"/>',
    book:'<path d="M4 5.5A2.5 2.5 0 016.5 3H20v15H6.5A2.5 2.5 0 004 20.5v-15z"/><path d="M4 20.5A2.5 2.5 0 006.5 18H20"/>',
    grid:'<rect x="3" y="3" width="7" height="7" rx="2"/><rect x="14" y="3" width="7" height="7" rx="2"/><rect x="3" y="14" width="7" height="7" rx="2"/><rect x="14" y="14" width="7" height="7" rx="2"/>',
    cart:'<circle cx="9" cy="20" r="1.4"/><circle cx="18" cy="20" r="1.4"/><path d="M3 4h2.5l2.2 11h10.6l1.7-8H6.2"/>',
    search:'<circle cx="11" cy="11" r="6.5"/><path d="M20 20l-4.2-4.2"/>',
    plus:'<path d="M12 5v14M5 12h14"/>',minus:'<path d="M5 12h14"/>',x:'<path d="M6 6l12 12M18 6L6 18"/>',
    mail:'<rect x="3" y="5" width="18" height="14" rx="3"/><path d="M4 7l8 6 8-6"/>',
    lock:'<rect x="5" y="11" width="14" height="9" rx="3"/><path d="M8 11V8a4 4 0 018 0v3"/>',
    key:'<circle cx="8" cy="15" r="4"/><path d="M11 12l9-9M16 7l3 3"/>',
    down:'<path d="M12 4v11M7.5 10.5L12 15l4.5-4.5M5 20h14"/>',
    headset:'<path d="M4 14v-2a8 8 0 0116 0v2"/><rect x="3" y="14" width="4" height="6" rx="2"/><rect x="17" y="14" width="4" height="6" rx="2"/>',
    arrow:'<path d="M5 12h14M13 6l6 6-6 6"/>',check:'<path d="M5 12.5l4.5 4.5L19 7.5"/>',
    card:'<rect x="3" y="5" width="18" height="14" rx="3"/><path d="M3 10h18M7 15h4"/>',
    wallet:'<path d="M4 7a2 2 0 012-2h11v4"/><rect x="3" y="7" width="18" height="13" rx="3"/><circle cx="16.5" cy="13.5" r="1"/>',
    bank:'<path d="M3 9l9-5 9 5M5 10v8M10 10v8M14 10v8M19 10v8M3 20h18"/>',
    left:'<path d="M19 12H5M11 6l-6 6 6 6"/>',
    pause:'<path d="M9 5v14M15 5v14"/>',
    play:'<path d="M8 5l11 7-11 7z"/>',
    dots:'<circle cx="5" cy="12" r="2"/><circle cx="12" cy="12" r="2"/><circle cx="19" cy="12" r="2"/>',
    user:'<circle cx="12" cy="8" r="4"/><path d="M4 21a8 8 0 0116 0"/>',
    info:'<circle cx="12" cy="12" r="9"/><path d="M12 11v6M12 7.5v.01"/>',
    chev:'<path d="M6 9l6 6 6-6"/>',
    chevr:'<path d="M9 6l6 6-6 6"/>',
    receipt:'<path d="M6 3h12v18l-3-2-3 2-3-2-3 2z"/><path d="M9 8h6M9 12h6"/>',
    bell:'<path d="M6 17V11a6 6 0 0112 0v6l1.5 2h-15z"/><path d="M10 21h4"/>',
    copy:'<rect x="8" y="8" width="12" height="12" rx="3"/><path d="M16 8V6a2 2 0 00-2-2H6a2 2 0 00-2 2v8a2 2 0 002 2h2"/>',
    eye:'<path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7S2 12 2 12z"/><circle cx="12" cy="12" r="3"/>',
    out:'<path d="M10 4H6a2 2 0 00-2 2v12a2 2 0 002 2h4M16 8l4 4-4 4M20 12H9"/>',
    device:'<rect x="3" y="5" width="18" height="12" rx="2"/><path d="M8 21h8M12 17v4"/>'
  };
  function svg(n,s,stroke,sw){return '<svg width="'+s+'" height="'+s+'" viewBox="0 0 24 24" fill="none" stroke="'+stroke+'" stroke-width="'+sw+'" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true">'+ICON[n]+'</svg>';}
  function icons(root){(root||document).querySelectorAll('[data-icon]').forEach(function(el){
    el.style.display='inline-flex';
    el.innerHTML=svg(el.dataset.icon,+el.dataset.size||20,el.dataset.stroke||'currentColor',el.dataset.sw||1.9);
  });}
  var catIcon={'All':'grid','Technolife':'device','Digikala':'cart','Electronics':'device','Clothing':'gift','Gaming':'game','Food':'card'};
  var fa=function(n){return n;};var tr=function(x){return x;};
  var fmt=function(n){return '$'+n.toFixed(2);};
  var $=function(id){return document.getElementById(id);};

  var cart;
  try{cart=JSON.parse(localStorage.getItem('digaily-cart'));}catch(e){}
  if(!cart||typeof cart!=='object'){cart={1:1,6:1};}
  function save(){try{localStorage.setItem('digaily-cart',JSON.stringify(cart));}catch(e){}}
  var cat='All', sub='', type='', query='';
  var LIMIT=8, showAll=false; /* how many products show before the "View all" button */
  function hf(c,s,t){return '#/shop/'+[c,s,t].filter(Boolean).map(encodeURIComponent).join('/');}

  function count(){var n=0;for(var k in cart)n+=cart[k];return n;}
  function total(){var t=0;P.forEach(function(p){if(cart[p.id])t+=p.price*cart[p.id];});return t;}
  function itemsText(n){return n===1?'1 item':n+' items';}

  /* categories + products */
  function renderCats(){
    var names=['All','Technolife','Digikala','Electronics','Clothing','Gaming','Food'];
    $('cats').innerHTML=names.map(function(n){
      var c=n==='All'?P.length:P.filter(function(p){return p.cat===n;}).length;
      return '<button type="button" class="glass cat lift" data-cat="'+n+'" aria-pressed="'+(n===cat)+'">'+svg(catIcon[n],20,'currentColor',1.9)+'<span>'+n+'</span><small>'+c+'</small></button>';
    }).join('');
  }
  function renderCrumbs(){
    var el=$('crumbs');
    if(cat==='All'&&!sub&&!type){el.hidden=true;el.innerHTML='';return;}
    var h='<a href="#/shop/All">All products</a>',sep='<span aria-hidden="true">/</span>';
    if(cat!=='All')h+=sep+(sub?'<a href="'+hf(cat)+'">'+cat+'</a>':'<span class="cur" aria-current="page">'+cat+'</span>');
    if(sub)h+=sep+(type?'<a href="'+hf(cat,sub)+'">'+sub+'</a>':'<span class="cur" aria-current="page">'+sub+'</span>');
    if(type)h+=sep+'<span class="cur" aria-current="page">'+type+'</span>';
    h+='<a class="clear" href="#/shop/All">Clear filter</a>';
    el.innerHTML=h;el.hidden=false;
  }
  function cardHTML(p){
    var t=TONE[p.cat], ic=icFor(p.cat);
    return '<article class="glass card lift"><div class="tile" style="background:'+t[0]+'"><span class="tag">'+p.cat+'</span>'+svg(ic,64,t[1],1.5)+'</div>'+
      '<div class="body"><div><div class="name">'+p.name+'</div><div class="meta">'+p.meta+'</div></div>'+
      '<div class="buy"><span class="price">'+fmt(p.price)+'</span><div class="acts">'+
      '<a class="viewbtn" href="#/product/'+p.id+'" aria-label="View '+p.name+'">View</a>'+
      '<button type="button" class="add" data-add="'+p.id+'" aria-label="Add '+p.name+' to cart">'+svg('plus',16,'#FFFBE6',2.2)+'<span>Add</span></button></div></div></div></article>';
  }
  function renderProduct(p){
    var t=TONE[p.cat],info=INFO[p.id]||['',[]];
    var rel=P.filter(function(x){return x.cat===p.cat&&x.id!==p.id;}).slice(0,4);
    $('v-product').innerHTML=
      '<div class="crumb"><a href="#/shop/All">Shop</a><span aria-hidden="true">/</span><a href="'+hf(p.cat)+'">'+p.cat+'</a><span aria-hidden="true">/</span><span>'+p.name+'</span></div>'+
      '<div class="pd"><div class="glass pd-art"><div class="tile" style="background:'+t[0]+'">'+svg(icFor(p.cat),150,t[1],1.3)+'</div></div>'+
      '<div class="pd-info"><span class="pd-tag">'+p.cat+' &middot; '+p.sub+'</span><h1>'+p.name+'</h1>'+
      '<div class="meta" style="font-size:16px">'+p.meta+'</div><div class="pd-price">'+fmt(p.price)+'</div>'+
      '<p class="pd-desc">'+info[0]+'</p>'+
      '<ul class="pd-list">'+info[1].map(function(x){return '<li><span class="ck">'+svg('check',16,'#FFFBE6',2.6)+'</span>'+x+'</li>';}).join('')+'</ul>'+
      '<div class="row"><button type="button" class="btn primary big" data-add="'+p.id+'">'+svg('plus',18,'#FFFBE6',2.2)+'<span>Add to cart</span></button>'+
      '<a class="btn glassy big" href="'+hf(p.cat)+'">Back to '+p.cat.toLowerCase()+'</a></div>'+
      '<div class="pd-note"><span class="glass">'+svg('mail',18,'currentColor',1.9)+'Sent to your inbox</span><span class="glass">'+svg('lock',18,'currentColor',1.9)+'Secure checkout</span><span class="glass">'+svg('headset',18,'currentColor',1.9)+'Friendly support</span></div>'+
      '</div></div>'+
      (rel.length?'<section class="pd-related"><h2>More in '+p.cat+'</h2><div class="grid">'+rel.map(cardHTML).join('')+'</div></section>':'');
  }
  function renderGrid(){
    renderCrumbs();
    var q=query.trim().toLowerCase();
    var list=P.filter(function(p){return (cat==='All'||p.cat===cat)&&(!sub||p.sub===sub)&&(!type||p.type===type)&&(!q||(p.name+' '+p.cat+' '+p.sub+' '+p.type+' '+p.meta).toLowerCase().indexOf(q)>-1);});
    var capped=!showAll&&!q&&list.length>LIMIT;
    var shown=capped?list.slice(0,LIMIT):list;
    $('grid').innerHTML=shown.map(cardHTML).join('');
    $('none').hidden=list.length>0;
    var moreOn=!q&&list.length>LIMIT;
    $('moreWrap').hidden=!moreOn;
    if(moreOn){
      $('moreBtn').setAttribute('aria-expanded',showAll?'true':'false');
      $('moreBtn').innerHTML='<span>'+(showAll?'Show fewer':'View all products ('+list.length+')')+'</span>'+svg('chev',18,'currentColor',2);
    }
  }
  $('moreBtn').addEventListener('click',function(){
    showAll=!showAll;renderGrid();
    if(showAll){$('live').textContent='Showing all products.';}
    else{$('shop').scrollIntoView({behavior:'smooth',block:'start'});}
  });

  /* cart lines (drawer + checkout summary) */
  function lineHTML(p,qty,editable){
    var t=TONE[p.cat];
    var top='<div class="top"><div><div class="ln">'+p.name+'</div><div class="lm">'+p.meta+'</div></div>'+
      (editable?'<button type="button" class="iconbtn sm" data-rm="'+p.id+'" aria-label="Remove '+p.name+' from cart">'+svg('x',16,'#34476B',2)+'</button>':'')+'</div>';
    var bot=editable
      ?'<div class="bot"><div class="step"><button type="button" data-dec="'+p.id+'" aria-label="Decrease quantity of '+p.name+'">'+svg('minus',16,'currentColor',2.2)+'</button><span>'+qty+'</span><button type="button" data-inc="'+p.id+'" aria-label="Increase quantity of '+p.name+'">'+svg('plus',16,'currentColor',2.2)+'</button></div><span class="lt">'+fmt(p.price*qty)+'</span></div>'
      :'<div class="bot"><span class="lm">Qty '+qty+'</span><span class="lt">'+fmt(p.price*qty)+'</span></div>';
    return '<div class="line"><div class="th" style="background:'+t[0]+'">'+svg('down',24,t[1],1.8)+'</div><div class="info">'+top+bot+'</div></div>';
  }
  function renderCart(){
    var n=count(), has=n>0;
    $('badge').textContent=n;
    $('cartBtn').setAttribute('aria-label','Open cart, '+itemsText(n));
    $('dCount').textContent=itemsText(n);
    $('dTotal').textContent=fmt(total());
    $('coTotal').textContent=fmt(total());
    var d=P.filter(function(p){return cart[p.id];}).map(function(p){return lineHTML(p,cart[p.id],true);}).join('');
    $('dLines').innerHTML=has?d:'<div class="empty">'+svg('cart',32,'#2F6FD0',1.7)+'<b>Your cart is empty</b><span>Add something from the shelf to get started.</span></div>';
    $('dSum').style.display=has?'':'none';
    $('coLines').innerHTML=has?P.filter(function(p){return cart[p.id];}).map(function(p){return lineHTML(p,cart[p.id],false);}).join(''):'<div class="empty"><b>Your cart is empty</b><span>Add something before checking out.</span><a class="btn primary" href="#/shop/All">Browse products</a></div>';
    $('placeBtn').disabled=!has;
    $('placeBtn').style.opacity=has?'':'.5';
    save();
  }

  /* drawer */
  var drawer=$('drawer'), scrim=$('scrim'), cartBtn=$('cartBtn'), lastFocus=null;
  drawer.inert=true;
  function openCart(){
    closeMega();
    lastFocus=document.activeElement;
    drawer.inert=false;drawer.classList.add('open');scrim.classList.add('open');
    drawer.setAttribute('aria-hidden','false');cartBtn.setAttribute('aria-expanded','true');
    setTimeout(function(){$('closeBtn').focus();},60);
  }
  function closeCart(restore){
    drawer.classList.remove('open');scrim.classList.remove('open');
    drawer.setAttribute('aria-hidden','true');cartBtn.setAttribute('aria-expanded','false');
    drawer.inert=true;
    if(restore!==false&&lastFocus&&lastFocus.focus)lastFocus.focus();
  }
  cartBtn.addEventListener('click',function(){drawer.classList.contains('open')?closeCart():openCart();});
  scrim.addEventListener('click',function(){closeCart();});
  $('closeBtn').addEventListener('click',function(){closeCart();});
  $('keepBtn').addEventListener('click',function(){closeCart();});
  $('goCheckout').addEventListener('click',function(){closeCart(false);});
  document.addEventListener('keydown',function(e){
    if(!drawer.classList.contains('open'))return;
    if(e.key==='Escape'){closeCart();return;}
    if(e.key==='Tab'){
      var f=drawer.querySelectorAll('button,a[href]');f=Array.prototype.filter.call(f,function(el){return el.offsetParent!==null;});
      if(!f.length)return;
      var first=f[0],last=f[f.length-1];
      if(e.shiftKey&&document.activeElement===first){e.preventDefault();last.focus();}
      else if(!e.shiftKey&&document.activeElement===last){e.preventDefault();first.focus();}
    }
  });

  /* clicks */
  document.addEventListener('click',function(e){
    var b=e.target.closest('[data-add],[data-inc],[data-dec],[data-rm],[data-cat]');
    if(!b)return;
    if(b.dataset.add){
      var id=b.dataset.add;cart[id]=(cart[id]||0)+1;renderCart();
      var bd=$('badge');bd.classList.remove('bump');void bd.offsetWidth;bd.classList.add('bump');
      var lbl=b.querySelector('span');
      if(lbl){var old=lbl.textContent;b.classList.add('done');lbl.textContent='Added';setTimeout(function(){b.classList.remove('done');lbl.textContent=old;},1100);}
      else{b.textContent='Added';}
      var p=P.filter(function(x){return String(x.id)===id;})[0];
      $('live').textContent=p.name+' added to cart. '+itemsText(count())+' in cart.';
    }else if(b.dataset.inc){cart[b.dataset.inc]+=1;renderCart();}
    else if(b.dataset.dec){cart[b.dataset.dec]=Math.max(1,cart[b.dataset.dec]-1);renderCart();}
    else if(b.dataset.rm){delete cart[b.dataset.rm];renderCart();}
    else if(b.dataset.cat){cat=b.dataset.cat;sub='';type='';showAll=false;renderCats();renderGrid();}
  });
  $('q').addEventListener('input',function(e){
    query=e.target.value;
    if(location.hash.indexOf('#/checkout')===0||location.hash==='#/done'||location.hash.indexOf('#/product')===0){location.hash='#/shop/'+cat;}
    renderGrid();
  });

  /* checkout form */
  $('coForm').addEventListener('submit',function(e){
    e.preventDefault();
    if(!count())return;
    var email=this.elements.email.value, name=this.elements.name.value.split(' ')[0];
    $('doneMsg').textContent='Thank you, '+name+'! Your keys and download links are on their way to '+email+'.';
    if(window.__addOrder)window.__addOrder(cart);
    cart={};renderCart();this.reset();
    location.hash='#/done';
  });

  /* tiny router: #/  #/shop/<category>  #/product/<id>  #/bundles  #/checkout  #/done */
  function route(){
    var h=location.hash.replace(/^#\/?/,'');
    var parts=h.split('/').map(function(x){try{return decodeURIComponent(x);}catch(err){return x;}});
    closeMega();
    var view=parts[0]==='account'?'account':parts[0]==='checkout'?'checkout':parts[0]==='done'?'done':parts[0]==='product'?'product':'home';
    var prod=null;
    if(view==='product'){
      prod=P.filter(function(x){return String(x.id)===parts[1];})[0];
      if(!prod){location.hash='#/shop/All';return;}
      renderProduct(prod);
    }
    ['home','product','checkout','done','account'].forEach(function(v){$('v-'+v).hidden=v!==view;});
    if(view==='home'){
      if(parts[0]==='shop'){
        var c=parts[1]||'All';
        if(!catIcon[c])c='All';
        cat=c;sub=c==='All'?'':(parts[2]||'');type=sub?(parts[3]||''):'';
        showAll=false;
        renderCats();renderGrid();
      }
      var target=parts[0]==='shop'?'shop':parts[0]==='bundles'?'bundles':parts[0]==='blog'?'blog':null;
      if(target){setTimeout(function(){$(target).scrollIntoView({behavior:'smooth',block:'start'});},30);}
      else{window.scrollTo(0,0);}
    }else{window.scrollTo(0,0);}
    if(view==='account')renderAccount(parts[1]||'overview');
    document.title=(view==='account'?'Account center':view==='checkout'?'Checkout':view==='done'?'Order confirmed':prod?prod.name:'Digaily')+' | Digaily';
  }
  window.addEventListener('hashchange',route);

  /* header categories menu: category > subcategory > type */
  var megaCat='Technolife', mega=$('mega'), catBtn=$('catBtn');
  function megaTree(){
    var t={};
    P.forEach(function(p){
      t[p.cat]=t[p.cat]||{};t[p.cat][p.sub]=t[p.cat][p.sub]||{};
      t[p.cat][p.sub][p.type]=(t[p.cat][p.sub][p.type]||0)+1;
    });
    return t;
  }
  function renderMegaL(){
    $('megaL').innerHTML=['Technolife','Digikala','Electronics','Clothing','Gaming','Food'].map(function(n){
      return '<li><button type="button" data-mc="'+n+'" aria-current="'+(n===megaCat)+'">'+svg(catIcon[n],20,'currentColor',1.9)+'<span style="flex:1">'+n+'</span>'+svg('chevr',16,'currentColor',2)+'</button></li>';
    }).join('');
  }
  function renderMegaR(){
    var subs=megaTree()[megaCat]||{};
    var total=P.filter(function(p){return p.cat===megaCat;}).length;
    $('megaR').innerHTML='<div class="mg-head"><h3>'+megaCat+'</h3><a class="mg-all" href="'+hf(megaCat)+'">Shop all '+megaCat.toLowerCase()+' ('+total+')</a></div>'+
      '<div class="mg-cols">'+Object.keys(subs).map(function(s){
        var leaves=subs[s],sc=0;Object.keys(leaves).forEach(function(k){sc+=leaves[k];});
        return '<div class="mg-col"><a class="mg-sub" href="'+hf(megaCat,s)+'"><span>'+s+'</span><small>'+sc+'</small></a>'+
          Object.keys(leaves).map(function(l){return '<a class="mg-leaf" href="'+hf(megaCat,s,l)+'"><span>'+l+'</span><small>'+leaves[l]+'</small></a>';}).join('')+'</div>';
      }).join('')+'</div>';
  }
  function setMegaCat(n){
    megaCat=n;
    Array.prototype.forEach.call($('megaL').querySelectorAll('[data-mc]'),function(b){b.setAttribute('aria-current',b.dataset.mc===n?'true':'false');});
    renderMegaR();
  }
  function openMega(){renderMegaL();renderMegaR();mega.hidden=false;catBtn.setAttribute('aria-expanded','true');}
  function closeMega(){mega.hidden=true;catBtn.setAttribute('aria-expanded','false');}
  catBtn.addEventListener('click',function(){mega.hidden?openMega():closeMega();});
  $('megaL').addEventListener('click',function(e){var b=e.target.closest('[data-mc]');if(b)setMegaCat(b.dataset.mc);});
  $('megaL').addEventListener('mouseover',function(e){var b=e.target.closest('[data-mc]');if(b&&b.dataset.mc!==megaCat)setMegaCat(b.dataset.mc);});
  mega.addEventListener('click',function(e){if(e.target.closest('a'))closeMega();});
  document.addEventListener('click',function(e){if(!mega.hidden&&!e.target.closest('#mega')&&!e.target.closest('#catBtn'))closeMega();});
  document.addEventListener('keydown',function(e){if(e.key==='Escape'&&!mega.hidden){closeMega();catBtn.focus();}});

  /* suggested-products slider */
  var SL=[
    {id:1,line:'A powerful phone for everything in your day.'},
    {id:2,line:'Hear every detail in your next game.'},
    {id:3,line:'A comfortable cotton essential for any day.'},
    {id:4,line:'A rich, freshly roasted start to your morning.'},
    {id:5,line:'Take your music and calls anywhere.'},
    {id:6,line:'Take control of your next match.'}
  ];
  var si=0,tm=null,hovering=false,
      playing=!(window.matchMedia&&window.matchMedia('(prefers-reduced-motion: reduce)').matches);
  function icFor(c){return c==='Gaming'?'game':c==='Electronics'||c==='Technolife'?'device':c==='Digikala'?'cart':c==='Food'?'card':'gift';}
  function buildSlider(){
    $('slTrack').innerHTML=SL.map(function(s,i){
      var p=P.filter(function(x){return x.id===s.id;})[0],t=TONE[p.cat];
      return '<div class="slide" role="group" aria-roledescription="slide" aria-label="'+(i+1)+' of '+SL.length+'">'+
        '<div class="sl-art" style="background:'+t[0]+'" aria-hidden="true">'+svg(icFor(p.cat),240,t[1],1.2)+'</div>'+
        '<div class="sl-text glass"><span class="sl-tag">'+p.cat+'</span><h2>'+p.name+'</h2><p>'+s.line+' '+p.meta+'.</p>'+
        '<div class="row" style="align-items:center"><span class="price">'+fmt(p.price)+'</span>'+
        '<button type="button" class="btn primary" data-add="'+p.id+'">'+svg('plus',16,'#FFFBE6',2.2)+'<span>Add to cart</span></button>'+
        '<a class="btn glassy" href="#/shop/'+p.cat+'">Browse '+p.cat.toLowerCase()+'</a></div></div></div>';
    }).join('');
    $('slDots').innerHTML=SL.map(function(s,i){
      return '<button type="button" class="dot" data-slide="'+i+'" aria-label="Go to slide '+(i+1)+'"><i></i></button>';
    }).join('');
    goSlide(0);setPlayIcon();tick();
  }
  function goSlide(n){
    si=(n+SL.length)%SL.length;
    $('slTrack').style.transform='translateX(-'+(si*100)+'%)';
    Array.prototype.forEach.call($('slTrack').children,function(el,i){el.inert=i!==si;el.setAttribute('aria-hidden',i!==si?'true':'false');});
    Array.prototype.forEach.call($('slDots').children,function(el,i){el.setAttribute('aria-current',i===si?'true':'false');});
  }
  function tick(){
    clearInterval(tm);tm=null;
    if(playing&&!hovering){tm=setInterval(function(){goSlide(si+1);},5000);}
  }
  function setPlayIcon(){
    $('slPlay').innerHTML=svg(playing?'pause':'play',18,'currentColor',2);
    $('slPlay').setAttribute('aria-label',playing?'Pause slideshow':'Play slideshow');
  }
  $('slPrev').addEventListener('click',function(){goSlide(si-1);tick();});
  $('slNext').addEventListener('click',function(){goSlide(si+1);tick();});
  $('slPlay').addEventListener('click',function(){playing=!playing;setPlayIcon();tick();});
  $('slDots').addEventListener('click',function(e){var d=e.target.closest('[data-slide]');if(d){goSlide(+d.dataset.slide);tick();}});
  var sl=$('slider');
  sl.addEventListener('mouseenter',function(){hovering=true;tick();});
  sl.addEventListener('mouseleave',function(){hovering=false;tick();});
  sl.addEventListener('focusin',function(){hovering=true;tick();});
  sl.addEventListener('focusout',function(){hovering=false;tick();});
  var sx=null;
  sl.addEventListener('touchstart',function(e){sx=e.touches[0].clientX;},{passive:true});
  sl.addEventListener('touchend',function(e){
    if(sx===null)return;var dx=e.changedTouches[0].clientX-sx;sx=null;
    if(Math.abs(dx)>50){goSlide(si+(dx<0?1:-1));tick();}
  });

  /* bottom-left dock: support / sign in / about */
  (function(){
    var dock=$('dock'),panel=$('dkPanel'),pill=$('dkPill'),items=$('dkItems'),trig=$('dkTrig'),open=null,user=null,hov=false,pin=false,kbd=false,t=null;
    try{user=localStorage.getItem('digaily-user');}catch(e){}
    var X='<button class="iconbtn sm dk-x" type="button" data-close aria-label="Close panel"><span data-icon="x" data-size="18" data-stroke="#0F2547" data-sw="2"></span></button>';
    function lbl(){$('dkSignLbl').textContent=user?user.split('@')[0]:'Sign in';place();if(window.__acRefresh)window.__acRefresh();}
    var views={
      support:function(){return X+'<h2 id="dkTitle">Support</h2><p>Stuck on an activation or an order? A real person will help.</p>'+
        '<div class="dk-links"><a href="#/"><span data-icon="key" data-size="20" data-stroke="#1E4FB8" data-sw="1.9"></span>Activation guide</a><a href="#/"><span data-icon="mail" data-size="20" data-stroke="#1E4FB8" data-sw="1.9"></span>Order status</a></div>'+
        '<form id="dkSup" novalidate><label class="field">Your email<input type="email" name="e" autocomplete="email" placeholder="you@example.com"></label><label class="field">How can we help?<textarea name="m" placeholder="Tell us what happened"></textarea></label><button class="btn primary full" type="submit">Send message</button><div class="dk-msg" id="dkMsg"></div></form>';},
      signin:function(){
        if(user)return X+'<h2 id="dkTitle">Your account</h2><p>Signed in as <b>'+user.replace(/[<>&"]/g,'')+'</b>.</p><a class="btn primary full" href="#/account"><span data-icon="user" data-size="18" data-stroke="#FFFBE6" data-sw="2"></span>Open account center</a><button class="btn glassy full" type="button" id="dkOut">Sign out</button>';
        return X+'<h2 id="dkTitle">Sign in</h2><p>Welcome back. Your orders and license keys are waiting.</p>'+
        '<form id="dkSign" novalidate><label class="field">Email<input type="email" name="e" autocomplete="email" placeholder="you@example.com"></label><label class="field">Password<input type="password" name="p" autocomplete="current-password" placeholder="Your password"></label><button class="btn primary full" type="submit">Sign in</button><div class="dk-msg" id="dkMsg"></div></form>';},
      about:function(){return X+'<h2 id="dkTitle">About Digaily</h2><p>Digaily sells digital goods for everyday life: software, games, gift cards and courses. Keys and downloads land in your inbox the moment you check out.</p><div class="dk-links"><a href="#/blog"><span data-icon="book" data-size="20" data-stroke="#1E4FB8" data-sw="1.9"></span>Read the blog</a><a href="#/shop/All"><span data-icon="grid" data-size="20" data-stroke="#1E4FB8" data-sw="1.9"></span>Browse the shop</a></div>';}
    };
    function fit(){items.style.maxWidth=dock.classList.contains('x')?items.scrollWidth+'px':'0px';}
    function sync(){
      var x=hov||pin||kbd||!!open;
      dock.classList.toggle('x',x);trig.setAttribute('aria-expanded',x?'true':'false');fit();
    }
    function place(){
      var b=open&&dock.querySelector('[data-dk="'+open+'"]');
      if(b){pill.style.left=b.offsetLeft+'px';pill.style.width=b.offsetWidth+'px';pill.classList.add('on');}
      else pill.classList.remove('on');
      fit();
    }
    function show(name){
      open=name;
      dock.querySelectorAll('[data-dk]').forEach(function(b){b.setAttribute('aria-expanded',b.dataset.dk===name?'true':'false');});
      if(!name){panel.hidden=true;panel.innerHTML='';sync();place();return;}
      panel.innerHTML=views[name]();panel.hidden=false;icons(panel);sync();
      requestAnimationFrame(place);
      var f=panel.querySelector('input,textarea');if(f&&matchMedia('(hover:hover)').matches)f.focus();
    }
    function reset(){hov=false;pin=false;kbd=false;show(null);}
    /* hover opens the circle; leaving closes it (unless a panel is open) */
    dock.addEventListener('mouseenter',function(){clearTimeout(t);hov=true;sync();});
    dock.addEventListener('mouseleave',function(){clearTimeout(t);t=setTimeout(function(){hov=false;sync();},250);});
    dock.addEventListener('focusin',function(e){if(e.target.matches(':focus-visible')){kbd=true;sync();}});
    dock.addEventListener('focusout',function(e){if(!dock.contains(e.relatedTarget)){kbd=false;sync();}});
    trig.addEventListener('click',function(){
      if(dock.classList.contains('x')&&pin){reset();}
      else{pin=true;sync();}
    });
    dock.addEventListener('click',function(e){var b=e.target.closest('[data-dk]');if(b)show(open===b.dataset.dk?null:b.dataset.dk);});
    panel.addEventListener('click',function(e){
      if(e.target.closest('[data-close]')){show(null);return;}
      if(e.target.closest('#dkOut')){user=null;try{localStorage.removeItem('digaily-user');}catch(x){}lbl();show('signin');return;}
      if(e.target.closest('a'))show(null);
    });
    panel.addEventListener('submit',function(e){
      e.preventDefault();var f=e.target,m=$('dkMsg'),em=f.e.value.trim(),ok=/^\S+@\S+\.\S+$/.test(em);
      if(!ok){m.textContent='Please enter a valid email address.';f.e.focus();return;}
      if(f.id==='dkSup'){if(!f.m.value.trim()){m.textContent='Please add a short message.';f.m.focus();return;}m.textContent='Thanks! We will reply to '+em+' soon.';f.reset();}
      else{if(!f.p.value){m.textContent='Please enter your password.';f.p.focus();return;}
        user=em;try{localStorage.setItem('digaily-user',em);}catch(x){}lbl();show('signin');}
    });
    document.addEventListener('keydown',function(e){if(e.key==='Escape'&&(open||dock.classList.contains('x'))){var b=open&&dock.querySelector('[data-dk="'+open+'"]');pin=false;show(null);b&&b.focus();}});
    document.addEventListener('click',function(e){if(!$('dockWrap').contains(e.target)&&dock.classList.contains('x'))reset();});
    window.addEventListener('resize',place);
    window.__dkSet=function(u){user=u;lbl();};
    lbl();
  })();


  /* ===== account center ===== */
  var AK={p:'digaily-profile',o:'digaily-orders',s:'digaily-prefs'};
  function ls(k,d){try{var v=JSON.parse(localStorage.getItem(k));return v==null?d:v;}catch(e){return d;}}
  function ss(k,v){try{localStorage.setItem(k,JSON.stringify(v));}catch(e){}}
  function esc(s){return String(s==null?'':s).replace(/[&<>"']/g,function(c){return {'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[c];});}
  function getUser(){try{return localStorage.getItem('digaily-user');}catch(e){return null;}}
  function setUser(u){try{u?localStorage.setItem('digaily-user',u):localStorage.removeItem('digaily-user');}catch(e){}if(window.__dkSet)window.__dkSet(u||null);}
  var SEED=[{id:'DG-10482',date:'Oct 28, 2026',items:{1:1,9:1}},{id:'DG-10391',date:'Oct 12, 2026',items:{4:1,14:1}},{id:'DG-10277',date:'Sep 30, 2026',items:{3:2}}];
  function orders(){return ls(AK.o,null)||SEED;}
  window.__addOrder=function(c){
    var items={},n=0;for(var k in c){items[k]=c[k];n++;}if(!n)return;
    var o=orders().slice();
    o.unshift({id:'DG-'+(10500+Math.floor(Math.random()*89000)),date:new Date().toLocaleDateString('en-US',{month:'short',day:'numeric',year:'numeric'}),items:items});
    ss(AK.o,o);
  };
  function pOf(id){return P.filter(function(x){return x.id===+id;})[0];}
  function oTotal(o){var t=0;for(var k in o.items){var p=pOf(k);if(p)t+=p.price*o.items[k];}return t;}
  function oCount(o){var n=0;for(var k in o.items)n+=o.items[k];return n;}
  function prof(){
    var u=getUser()||'',p=ls(AK.p,{}),nm=p.name||u.split('@')[0].replace(/[._-]+/g,' ').replace(/\b\w/g,function(c){return c.toUpperCase();});
    return {name:nm||'Friend',phone:p.phone||'',country:p.country||'United States',since:p.since||new Date().toLocaleDateString('en-US',{month:'short',year:'numeric'})};
  }
  function keyFor(o,pid){
    var h=2166136261,s=o+'-'+pid,r='',A='ABCDEFGHJKLMNPQRSTUVWXYZ23456789',i;
    for(i=0;i<s.length;i++)h=Math.imul(h^s.charCodeAt(i),16777619)>>>0;
    for(i=0;i<16;i++){h=Math.imul(h^(i+7),16777619)>>>0;h^=h>>>13;r+=A[(h>>>0)%32];if(i%4===3&&i<15)r+='-';}
    return r;
  }
  function mask(k){return '\u2022\u2022\u2022\u2022-\u2022\u2022\u2022\u2022-\u2022\u2022\u2022\u2022-'+k.slice(-4);}
  var TABS=[['overview','Overview','grid'],['orders','Orders','receipt'],['licenses','Licenses & downloads','key'],['profile','Profile','user'],['security','Security','lock'],['prefs','Preferences','bell']];
  var curTab='overview',vA=$('v-account'),COUNTRIES=['United States','Canada','United Kingdom','Germany','France','Spain','Australia','Iran','Other'];
  function say(t,err){var m=$('acMsg');if(m){m.textContent=t;m.className='ac-msg'+(err?' err':'');}$('live').textContent=t;}
  function ico(n,bg,fg,sz){return '<div class="ico" style="background:'+bg+'">'+svg(n,sz||22,fg,1.9)+'</div>';}
  function ordHTML(o,full){
    var lines=Object.keys(o.items).map(function(k){var p=pOf(k);return p?lineHTML(p,o.items[k],false):'';}).join('');
    return '<article class="ord"><div class="ord-h"><div><b>'+esc(o.id)+'</b><span class="lm">'+esc(o.date)+' &middot; '+itemsText(oCount(o))+'</span></div><div class="ord-r"><span class="pill ok">Delivered</span><b class="lt">'+fmt(oTotal(o))+'</b></div></div>'+
      (full?'<div class="ord-lines">'+lines+'</div><div class="row"><button type="button" class="btn primary sm" data-act="reorder" data-oid="'+esc(o.id)+'">'+svg('plus',16,'#FFFBE6',2.2)+'Reorder</button><a class="btn glassy sm" href="#/account/licenses">View keys</a></div>':'')+'</article>';
  }
  function fieldH(l,n,t,v,x){return '<label class="field">'+l+'<input name="'+n+'" type="'+t+'" value="'+esc(v)+'" '+(x||'')+'></label>';}
  function switchH(k,t,d,on){return '<div class="prefrow"><div><b>'+t+'</b><span>'+d+'</span></div><button type="button" class="switch" role="switch" aria-checked="'+on+'" aria-label="'+t+'" data-pref="'+k+'"></button></div>';}
  var TAB={
    overview:function(u,p){
      var os=orders(),spent=0,keys=0;os.forEach(function(o){spent+=oTotal(o);keys+=Object.keys(o.items).length;});
      return '<div class="glass-dark ac-welcome"><div><h2>Welcome back, '+esc(p.name.split(' ')[0])+'</h2><p>Your keys and download links are always one click away. Member since '+esc(p.since)+'.</p></div><a class="btn cream big" href="#/account/licenses">Open my keys '+svg('arrow',18,'currentColor',2)+'</a></div>'+
        '<div class="stats"><div class="stat">'+ico('receipt','#1E4FB8','#FFFBE6')+'<b>'+os.length+'</b><span>Orders placed</span></div><div class="stat">'+ico('key','#2F6FD0','#FFFBE6')+'<b>'+keys+'</b><span>Active licenses</span></div><div class="stat">'+ico('wallet','#FFD84D','#0F2547')+'<b>'+fmt(spent)+'</b><span>Total spent</span></div><div class="stat">'+ico('cart','#BCD4F7','#0F2547')+'<b>'+count()+'</b><span>Items in cart</span></div></div>'+
        '<div><h3>Recent orders</h3><div class="ord-lines" style="margin-top:12px">'+os.slice(0,3).map(function(o){return ordHTML(o,false);}).join('')+'</div></div>'+
        '<div class="quick"><a class="glass lift" href="#/account/licenses">'+ico('down','#1E4FB8','#FFFBE6',20)+'Download my purchases</a><a class="glass lift" href="#/account/profile">'+ico('user','#2F6FD0','#FFFBE6',20)+'Update my profile</a><a class="glass lift" href="#/account/security">'+ico('lock','#BCD4F7','#0F2547',20)+'Secure my account</a></div>';
    },
    orders:function(){
      var os=orders();
      return '<div><h2>Orders</h2><p class="ac-sub">Everything you have bought, newest first.</p></div>'+(os.length?'<div class="ord-lines">'+os.map(function(o){return ordHTML(o,true);}).join('')+'</div>':'<div class="empty"><b>No orders yet</b><span>Your purchases will show up here.</span><a class="btn primary" href="#/shop/All">Browse products</a></div>');
    },
    licenses:function(){
      var rows=[];
      orders().forEach(function(o){Object.keys(o.items).forEach(function(k){var p=pOf(k);if(!p)return;var key=keyFor(o.id,k),t=TONE[p.cat],sh=shown[o.id+k];
        rows.push('<div class="keyrow" data-o="'+esc(o.id)+'" data-p="'+k+'"><div class="th" style="background:'+t[0]+'">'+svg(icFor(p.cat),26,t[1],1.7)+'</div><div class="inf"><b>'+p.name+'</b><span class="lm">'+(p.cat==='Courses'?'Access code':'License key')+' &middot; '+esc(o.id)+'</span></div><code class="key">'+(sh?key:mask(key))+'</code><div class="btns"><button type="button" class="btn glassy sm" data-act="reveal">'+svg('eye',16,'currentColor',2)+'<span>'+(sh?'Hide':'Reveal')+'</span></button><button type="button" class="btn glassy sm" data-act="copy">'+svg('copy',16,'currentColor',2)+'Copy</button><button type="button" class="btn primary sm" data-act="dl">'+svg('down',16,'#FFFBE6',2)+'Download</button></div></div>');});});
      return '<div><h2>Licenses &amp; downloads</h2><p class="ac-sub">Reveal a key only when you need it. Keep it private.</p></div><div class="ord-lines">'+rows.join('')+'</div>';
    },
    profile:function(u,p){
      return '<div><h2>Profile</h2><p class="ac-sub">Used for your receipts and key delivery.</p></div><form id="acProfile" novalidate><div class="fields">'+fieldH('Full name','name','text',p.name,'autocomplete="name"')+fieldH('Email','email','email',u,'autocomplete="email"')+fieldH('Phone (optional)','phone','tel',p.phone,'autocomplete="tel" placeholder="+1 555 0100"')+
        '<label class="field">Country<select name="country">'+COUNTRIES.map(function(c){return '<option'+(c===p.country?' selected':'')+'>'+c+'</option>';}).join('')+'</select></label></div><div class="row"><button class="btn primary" type="submit">Save changes</button></div></form>';
    },
    security:function(){
      var s=ls(AK.s,{}),others=!s.noOthers;
      var S=function(n,a,b,i){return '<div class="sess">'+ico(i||'device','rgba(30,79,184,.12)','#1E4FB8',20)+'<div><b>'+n+'</b><span>'+a+'</span></div>'+(b?'<span class="pill ok" style="margin-left:auto">This device</span>':'')+'</div>';};
      return '<div><h2>Security</h2><p class="ac-sub">Keep your account and keys safe.</p></div>'+
        '<form id="acPass" novalidate><h3>Change password</h3><div class="fields">'+fieldH('Current password','cur','password','','autocomplete="current-password"')+fieldH('New password','nw','password','','autocomplete="new-password" placeholder="At least 8 characters"')+fieldH('Confirm new password','cf','password','','autocomplete="new-password"')+'</div><div class="row"><button class="btn primary" type="submit">Update password</button></div></form>'+
        switchH('twofa','Two-step sign in','Ask for a code from your phone when you sign in.',!!s.twofa)+
        '<div><h3>Signed-in devices</h3><div class="ord-lines" style="margin-top:12px">'+S('Browser on this computer','Active now',true)+(others?S('Windows &middot; Chrome','Last active 2 days ago')+S('iPhone &middot; Safari','Last active 6 days ago'):'')+'</div></div>'+
        (others?'<div class="row"><button type="button" class="btn glassy" data-act="others">Sign out other devices</button></div>':'');
    },
    prefs:function(){
      var s=ls(AK.s,{}),g=function(k,d){return s[k]==null?d:s[k];};
      return '<div><h2>Preferences</h2><p class="ac-sub">Choose what we send you. Changes save automatically.</p></div>'+
        switchH('mOrders','Order and delivery emails','Receipts, keys and download links.',g('mOrders',true))+
        switchH('mNews','Tips and new arrivals','Short, occasional product news.',g('mNews',true))+
        switchH('mPromo','Deals and bundles','Early access to discounts.',g('mPromo',false));
    }
  };
  function renderAccount(tab){
    if(!TAB[tab])tab='overview';curTab=tab;
    var u=getUser(),head='<div class="crumb"><a href="#/">Home</a><span aria-hidden="true">/</span><span>Account center</span></div><h1 class="page-title">Account center</h1>';
    if(!u){
      vA.innerHTML=head+'<section class="glass ac-gate"><div class="ico" style="background:#1E4FB8">'+svg('user',28,'#FFFBE6',1.8)+'</div><div><h2 style="font-size:28px">Sign in to your account</h2><p class="ac-sub">See your orders, license keys and downloads in one place.</p></div><form id="acGate" novalidate>'+fieldH('Email','e','email','','autocomplete="email" placeholder="you@example.com"')+fieldH('Password','p','password','','autocomplete="current-password" placeholder="Your password"')+'<button class="btn primary big full" type="submit">Sign in</button><div class="ac-msg" id="acMsg" role="status"></div></form><p class="hint">Demo store: any email and password will work.</p></section>';
      return;
    }
    var p=prof(),ini=p.name.split(/\s+/).map(function(w){return w[0]||'';}).join('').slice(0,2).toUpperCase();
    vA.innerHTML=head+'<div class="ac"><aside class="glass ac-side"><div class="ac-user"><div class="avatar" aria-hidden="true">'+esc(ini)+'</div><div><b>'+esc(p.name)+'</b><span>'+esc(u)+'</span></div></div><nav class="ac-nav" aria-label="Account sections">'+
      TABS.map(function(t){return '<a href="#/account/'+t[0]+'"'+(t[0]===tab?' aria-current="page"':'')+'>'+svg(t[2],20,'currentColor',1.9)+t[1]+'</a>';}).join('')+'<hr><button type="button" data-act="out">'+svg('out',20,'currentColor',1.9)+'Sign out</button></nav></aside>'+
      '<section class="glass ac-main" aria-label="'+esc(TABS.filter(function(t){return t[0]===tab;})[0][1])+'">'+TAB[tab](u,p)+'<div class="ac-msg" id="acMsg" role="status"></div></section></div>';
  }
  var shown={};
  window.__acRefresh=function(){if(!vA.hidden)renderAccount(curTab);};
  vA.addEventListener('click',function(e){
    var b=e.target.closest('[data-act],[data-pref]');if(!b)return;
    if(b.dataset.pref){var s=ls(AK.s,{}),on=b.getAttribute('aria-checked')!=='true';b.setAttribute('aria-checked',on);s[b.dataset.pref]=on;ss(AK.s,s);say('Saved.');return;}
    var a=b.dataset.act,row=b.closest('.keyrow');
    if(a==='out'){setUser(null);location.hash='#/account';}
    else if(a==='reorder'){var o=orders().filter(function(x){return x.id===b.dataset.oid;})[0];if(o){for(var k in o.items)cart[k]=(cart[k]||0)+o.items[k];renderCart();openCart();}}
    else if(a==='reveal'){var id=row.dataset.o,pid=row.dataset.p,k2=keyFor(id,pid);shown[id+pid]=!shown[id+pid];row.querySelector('.key').textContent=shown[id+pid]?k2:mask(k2);b.querySelector('span').textContent=shown[id+pid]?'Hide':'Reveal';}
    else if(a==='copy'){var kk=keyFor(row.dataset.o,row.dataset.p);try{navigator.clipboard.writeText(kk).then(function(){say('Key copied.');},function(){say('Copy failed. Reveal and copy it by hand.',true);});}catch(x){say('Copy failed. Reveal and copy it by hand.',true);}}
    else if(a==='dl'){say('Download started for '+pOf(row.dataset.p).name+'.');}
    else if(a==='others'){var s2=ls(AK.s,{});s2.noOthers=true;ss(AK.s,s2);renderAccount(curTab);say('Other devices were signed out.');}
  });
  vA.addEventListener('submit',function(e){
    e.preventDefault();var f=e.target,m=$('acMsg'),re=/^\S+@\S+\.\S+$/;
    if(f.id==='acGate'){
      if(!re.test(f.e.value.trim())){say('Please enter a valid email address.',true);f.e.focus();return;}
      if(!f.p.value){say('Please enter your password.',true);f.p.focus();return;}
      setUser(f.e.value.trim());
    }else if(f.id==='acProfile'){
      if(!f.name.value.trim()){say('Please enter your name.',true);f.name.focus();return;}
      if(!re.test(f.email.value.trim())){say('Please enter a valid email address.',true);f.email.focus();return;}
      ss(AK.p,{name:f.name.value.trim(),phone:f.phone.value.trim(),country:f.country.value,since:prof().since});
      if(f.email.value.trim()!==getUser())setUser(f.email.value.trim());else renderAccount('profile');
      say('Profile saved.');
    }else if(f.id==='acPass'){
      if(!f.cur.value){say('Enter your current password.',true);f.cur.focus();return;}
      if(f.nw.value.length<8){say('Use at least 8 characters for the new password.',true);f.nw.focus();return;}
      if(f.nw.value!==f.cf.value){say('The two new passwords do not match.',true);f.cf.focus();return;}
      f.reset();say('Password updated.');
    }
  });


  /* ===== theme toggle ===== */
  (function(){
    var root=document.documentElement,btn=$('themeBtn');
    function apply(t,persist){
      root.setAttribute('data-theme',t);
      btn.setAttribute('aria-pressed',t==='dark'?'true':'false');
      btn.setAttribute('aria-label',t==='dark'?'Switch to light mode':'Switch to dark mode');
      var m=document.querySelector('meta[name=theme-color]');if(m)m.content=t==='dark'?'#0A1224':'#FFF2C2';
      if(persist){try{localStorage.setItem('digaily-theme',t);}catch(e){}}
    }
    apply(root.getAttribute('data-theme')||'light',false);
    btn.addEventListener('click',function(){
      var t=root.getAttribute('data-theme')==='dark'?'light':'dark';
      root.classList.add('theme-anim');apply(t,true);
      setTimeout(function(){root.classList.remove('theme-anim');},450);
      $('live').textContent=t==='dark'?'Dark mode on.':'Light mode on.';
    });
    var mq=window.matchMedia&&matchMedia('(prefers-color-scheme: dark)');
    if(mq&&mq.addEventListener)mq.addEventListener('change',function(e){var s=null;try{s=localStorage.getItem('digaily-theme');}catch(x){}if(!s)apply(e.matches?'dark':'light',false);});
  })();

  /* ===== showcase: products -> details -> related ===== */
  var RT=document.documentElement.dir==='rtl'?1:-1,SXF=[1,2,3,4,5,9,13,14],SXS=['Products','Details','Related products'],sxi=0,sxp=1;
  function sxCard(p){
    var t=TONE[p.cat];
    return '<article class="glass card lift sx-card'+(p.id===sxp?' sel':'')+'"><div class="tile" style="background:'+t[0]+'"><span class="tag">'+tr(p.cat)+'</span>'+svg(icFor(p.cat),56,t[1],1.5)+'</div>'+
      '<div class="body"><div><div class="name">'+p.name+'</div><div class="meta">'+p.meta+'</div></div><div class="buy"><span class="price">'+fmt(p.price)+'</span><div class="acts">'+
      '<button type="button" class="viewbtn" data-pick="'+p.id+'" aria-label="Details of '+p.name+'">Details</button>'+
      '<button type="button" class="add" data-add="'+p.id+'" aria-label="Add '+p.name+' to cart">'+svg('plus',16,'#FFFBE6',2.2)+'<span>Add</span></button></div></div></div></article>';
  }
  function renderSx(){
    var p=pOf(sxp),t=TONE[p.cat],info=INFO[p.id],rel=P.filter(function(x){return x.cat===p.cat&&x.id!==p.id;});
    P.forEach(function(x){if(x.cat!==p.cat&&x.id!==p.id&&rel.length<4)rel.push(x);});
    $('sxP0').innerHTML='<div class="sx-grid">'+SXF.map(function(id){return sxCard(pOf(id));}).join('')+'</div>';
    $('sxP1').innerHTML='<div class="sx-det"><div class="sx-art" style="background:'+t[0]+'">'+svg(icFor(p.cat),150,t[1],1.3)+'</div><div class="sx-info"><span class="pd-tag">'+tr(p.cat)+' &middot; '+tr(p.sub)+'</span><h3>'+p.name+'</h3><div class="meta">'+p.meta+'</div><div class="pd-price">'+fmt(p.price)+'</div><p class="pd-desc">'+info[0]+'</p>'+
      '<ul class="pd-list">'+info[1].map(function(x){return '<li><span class="ck">'+svg('check',16,'#FFFBE6',2.6)+'</span>'+x+'</li>';}).join('')+'</ul>'+
      '<div class="row"><button type="button" class="btn primary" data-add="'+p.id+'">'+svg('plus',18,'#FFFBE6',2.2)+'<span>Add to cart</span></button><button type="button" class="btn glassy" data-sx="2">Related products</button></div></div></div>';
    $('sxP2').innerHTML='<h3 class="sx-h">Related to “'+p.name+'”</h3><div class="sx-grid">'+rel.slice(0,4).map(sxCard).join('')+'</div>';
    $('sxSteps').innerHTML=SXS.map(function(s,i){return '<button type="button" data-sx="'+i+'"'+(i===sxi?' aria-current="step"':'')+'><b>'+fa(i+1)+'</b><span>'+s+'</span></button>';}).join('');
  }
  function goSx(n){
    sxi=Math.max(0,Math.min(2,n));
    $('sxTrack').style.transform='translateX('+(RT*sxi*100)+'%)';
    [0,1,2].forEach(function(i){var el=$('sxP'+i);el.inert=i!==sxi;el.setAttribute('aria-hidden',i!==sxi?'true':'false');});
    Array.prototype.forEach.call($('sxSteps').children,function(el,i){if(i===sxi)el.setAttribute('aria-current','step');else el.removeAttribute('aria-current');});
    $('sxPrev').disabled=sxi===0;$('sxNext').disabled=sxi===2;
    $('sxView').style.height=$('sxP'+sxi).offsetHeight+'px';
  }
  function buildShowcase(){renderSx();icons($('showcase'));goSx(0);}
  $('showcase').addEventListener('click',function(e){
    var k=e.target.closest('[data-pick]');
    if(k){sxp=+k.dataset.pick;renderSx();goSx(1);return;}
    var s=e.target.closest('[data-sx]');if(s)goSx(+s.dataset.sx);
  });
  $('sxPrev').addEventListener('click',function(){goSx(sxi-1);});
  $('sxNext').addEventListener('click',function(){goSx(sxi+1);});
  $('showcase').addEventListener('keydown',function(e){
    if(/INPUT|TEXTAREA|SELECT/.test(e.target.tagName))return;
    var nx=RT>0?'ArrowLeft':'ArrowRight',pv=RT>0?'ArrowRight':'ArrowLeft';if(e.key===nx)goSx(sxi+1);else if(e.key===pv)goSx(sxi-1);
  });
  var sxx=null;
  $('sxView').addEventListener('touchstart',function(e){sxx=e.touches[0].clientX;},{passive:true});
  $('sxView').addEventListener('touchend',function(e){
    if(sxx===null)return;var dx=e.changedTouches[0].clientX-sxx;sxx=null;
    if(Math.abs(dx)>60)goSx(sxi+(dx*RT>0?1:-1));
  });
  window.addEventListener('resize',function(){goSx(sxi);});
  if(document.fonts&&document.fonts.ready)document.fonts.ready.then(function(){goSx(sxi);});

  icons();buildSlider();buildShowcase();renderCats();renderGrid();renderCart();route();
})();