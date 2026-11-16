(function () {
  'use strict';

  var pairs = [
    // Header and navigation
    ['Categories', 'دسته‌بندی‌ها'], ['Main', 'منوی اصلی'], ['Software', 'نرم‌افزار'], ['Games', 'بازی‌ها'],
    ['Gift cards', 'گیفت‌کارت'], ['Courses', 'دوره‌ها'], ['Bundles', 'بسته‌ها'], ['Blog', 'وبلاگ'],
    ['Login', 'ورود'], ['Account Center', 'مرکز حساب کاربری'], ['Account center', 'مرکز حساب کاربری'],
    ['Search products', 'جستجوی محصولات'], ['Search Digaily', 'جستجو در دیجیلی'], ['Cart', 'سبد خرید'],
    ['Open cart', 'باز کردن سبد خرید'], ['Open account center', 'باز کردن مرکز حساب کاربری'],
    ['Digaily home', 'صفحه اصلی دیجیلی'], ['Open support, sign in and about', 'باز کردن پشتیبانی، ورود و درباره ما'],
    ['Support, account and about', 'پشتیبانی، حساب کاربری و درباره ما'],
    ['Suggested products', 'محصولات پیشنهادی'], ['Pause slideshow', 'توقف اسلایدشو'],
    ['Previous slide', 'اسلاید قبلی'], ['Next slide', 'اسلاید بعدی'],

    // Home page
    ['Software, games, gift cards and courses', 'نرم‌افزار، بازی، گیفت‌کارت و دوره'],
    ['Shop digital.', 'خرید دیجیتال.'], ['Delivered daily.', 'تحویل هر روز.'],
    ['Pick it up, pay securely, and get your license key or download link straight to your inbox. Everything digital, in one calm place.', 'محصول را انتخاب کنید، امن پرداخت کنید و کلید لایسنس یا لینک دانلود را مستقیم در ایمیل خود بگیرید. همه محصولات دیجیتال، در یک مکان ساده.'],
    ['Start shopping', 'شروع خرید'], ['See bundles', 'مشاهده بسته‌ها'], ['Sent to your inbox', 'ارسال به ایمیل شما'],
    ['Secure checkout', 'پرداخت امن'], ['Instant license keys', 'کلید لایسنس فوری'],
    ['Find your next download', 'دانلود بعدی خود را پیدا کنید'],
    ['Browse by category, add to your cart, and open it from the top bar when you are ready.', 'دسته‌بندی را مرور کنید، محصول را به سبد خرید بیفزایید و هر زمان آماده بودید از نوار بالا سبد را باز کنید.'],
    ['Filter by category', 'فیلتر بر اساس دسته‌بندی'], ['Current filter', 'فیلتر فعلی'],
    ['No products match your search. Try another word.', 'محصولی با جستجوی شما پیدا نشد. واژه دیگری را امتحان کنید.'],
    ['Bundle it up', 'یک بسته بسازید'],
    ['Pair a software license with a course or a game pack and check out in one go.', 'یک لایسنس نرم‌افزار را با دوره یا بسته بازی ترکیب کنید و همه را یک‌جا بخرید.'],
    ['Explore bundles', 'مشاهده بسته‌ها'], ['Why people shop Digaily', 'چرا مردم از دیجیلی خرید می‌کنند؟'],
    ['Instant delivery', 'تحویل فوری'],
    ['Keys and download links arrive in your inbox as soon as payment clears.', 'به‌محض تأیید پرداخت، کلیدها و لینک‌های دانلود به ایمیل شما می‌رسند.'],
    ['Secure checkout', 'پرداخت امن'], ['Pay with the method you trust, with your details kept private.', 'با روش مورداعتماد خود پرداخت کنید و اطلاعاتتان را خصوصی نگه دارید.'],
    ['Friendly support', 'پشتیبانی دوستانه'],
    ['Stuck on an activation? A real person is a message away.', 'برای فعال‌سازی به مشکل خوردید؟ فقط یک پیام با پشتیبانی فاصله دارید.'],
    ['From the Digaily blog', 'از وبلاگ دیجیلی'],
    ['Buying guides, learning tips and how-tos for your digital life.', 'راهنمای خرید، نکات آموزشی و آموزش‌های کاربردی برای زندگی دیجیتال شما.'],
    ['Guides', 'راهنماها'], ['Learning', 'آموزش'], ['Gifts', 'هدایا'], ['Help', 'راهنمایی'],
    ['How to choose the right antivirus for your devices', 'چطور آنتی‌ویروس مناسب دستگاه‌های خود را انتخاب کنید'],
    ['What to look for in a license, how many devices you need, and why a one-year plan is a good place to start.', 'در لایسنس به چه نکاتی توجه کنید، به چند دستگاه نیاز دارید و چرا طرح یک‌ساله شروع خوبی است.'],
    ['Learn a language on a busy schedule', 'در برنامه شلوغ خود زبان یاد بگیرید'],
    ['Short, regular lessons beat long cram sessions. Here is how to fit them into your day.', 'درس‌های کوتاه و منظم بهتر از مطالعه فشرده و طولانی هستند. این‌طور آن‌ها را در برنامه روزانه جا دهید.'],
    ['A quick guide to thoughtful digital gifts', 'راهنمایی کوتاه برای انتخاب هدیه دیجیتال'],
    ['Gift cards, game keys and courses: how to match the right digital gift to the person.', 'گیفت‌کارت، کلید بازی و دوره؛ چطور هدیه دیجیتال مناسبی برای هر فرد انتخاب کنید.'],
    ['Activating your game key, step by step', 'فعال‌سازی کلید بازی، قدم‌به‌قدم'],
    ['From the email in your inbox to your first level: a simple walkthrough for redeeming keys.', 'از ایمیل دریافتی تا ورود به اولین مرحله: راهنمای ساده استفاده از کلید بازی.'],
    ['Read article', 'خواندن مقاله'],

    // Footer and shared forms
    ['Digital goods for everyday life, delivered to your inbox.', 'کالاهای دیجیتال روزمره، مستقیم در ایمیل شما.'],
    ['Email address', 'نشانی ایمیل'], ['Your email', 'ایمیل شما'], ['Subscribe', 'عضویت'],
    ['Shop', 'فروشگاه'], ['Help', 'راهنما'], ['Company', 'شرکت'], ['Order status', 'وضعیت سفارش'],
    ['Activation guide', 'راهنمای فعال‌سازی'], ['Contact us', 'تماس با ما'], ['About Digaily', 'درباره دیجیلی'],
    ['Terms', 'شرایط استفاده'], ['Privacy', 'حریم خصوصی'], ['All rights reserved.', 'تمامی حقوق محفوظ است.'],

    // Product categories, attributes and catalog
    ['All', 'همه'], ['Technolife', 'تکنولایف'], ['Digikala', 'دیجی‌کالا'], ['Electronics', 'لوازم الکترونیکی'],
    ['Clothing', 'پوشاک'], ['Gaming', 'بازی و سرگرمی'], ['Food', 'مواد غذایی'],
    ['Phones', 'تلفن‌های همراه'], ['Smartphones', 'گوشی هوشمند'], ['Accessories', 'لوازم جانبی'],
    ['Headsets', 'هدست'], ['T-Shirts', 'تی‌شرت'], ['Pantry', 'مواد غذایی'], ['Coffee', 'قهوه'],
    ['Audio', 'صوتی'], ['Headphones', 'هدفون'], ['Controllers', 'کنترلر'], ['Wearables', 'لوازم پوشیدنی'],
    ['Smartwatches', 'ساعت هوشمند'], ['Tops', 'لباس بالاتنه'], ['Hoodies', 'هودی'], ['Computers', 'رایانه'],
    ['Monitors', 'مانیتور'], ['Shoes', 'کفش'], ['Sneakers', 'کتانی'], ['Snacks', 'تنقلات'],
    ['Chocolate', 'شکلات'], ['Bags', 'کیف'], ['Games', 'بازی‌ها'], ['Racing', 'مسابقه‌ای'], ['Olive Oil', 'روغن زیتون'],
    ['Technolife Nova Smartphone', 'گوشی هوشمند نوا تکنولایف'], ['Wireless Gaming Headset', 'هدست بی‌سیم گیمینگ'],
    ['Digikala Cotton T-Shirt', 'تی‌شرت نخی دیجی‌کالا'], ['Espresso Coffee Beans', 'دانه قهوه اسپرسو'],
    ['Bluetooth Earbuds', 'هدفون بلوتوثی'], ['Wireless Game Controller', 'کنترلر بی‌سیم بازی'],
    ['Technolife Smartwatch', 'ساعت هوشمند تکنولایف'], ['Everyday Zip Hoodie', 'هودی زیپ‌دار روزمره'],
    ['4K Computer Monitor', 'مانیتور کامپیوتر 4K'], ['Lightweight Running Sneakers', 'کتانی سبک دویدن'],
    ['Dark Chocolate Selection', 'مجموعه شکلات تلخ'], ['Digikala Everyday Backpack', 'کوله‌پشتی روزمره دیجی‌کالا'],
    ['Starlight Racing Game', 'بازی مسابقه‌ای استارلایت'], ['Extra-Virgin Olive Oil', 'روغن زیتون فرابکر'],
    ['256 GB storage, 8 GB RAM', 'حافظه ۲۵۶ گیگابایت، رم ۸ گیگابایت'], ['Wireless surround sound', 'صدای فراگیر بی‌سیم'],
    ['Soft cotton, multiple sizes', 'نخ نرم، در اندازه‌های مختلف'], ['1 kg, freshly roasted', 'یک کیلوگرم، تازه‌برشت'],
    ['Noise reduction, charging case', 'کاهش نویز، همراه با کیس شارژ'], ['Compatible with PC and console', 'سازگار با رایانه و کنسول'],
    ['Fitness tracking, 5-day battery', 'ردیابی فعالیت، باتری ۵ روزه'], ['Fleece lining, multiple sizes', 'آستر گرم، در اندازه‌های مختلف'],
    ['27-inch UHD display', 'نمایشگر UHD بیست‌وهفت اینچی'], ['Breathable mesh, cushioned sole', 'رویه مشبک و زیره نرم'],
    ['Assorted 70% cocoa bars', 'مخلوط شکلات با ۷۰٪ کاکائو'], ['Water-resistant, 20 L capacity', 'ضدآب، ظرفیت ۲۰ لیتر'],
    ['Console edition', 'نسخه کنسول'], ['Cold-pressed, 750 ml', 'پرس سرد، ۷۵۰ میلی‌لیتر'],

    // Product detail and shop actions (English/Persian source phrases)
    ['All products', 'همه محصولات'], ['Clear filter', 'حذف فیلتر'], ['View', 'مشاهده'], ['Add', 'افزودن'],
    ['Add to cart', 'افزودن به سبد خرید'], ['View all products', 'مشاهده همه محصولات'], ['Show fewer', 'نمایش کمتر'],
    ['Showing all products.', 'همه محصولات نمایش داده شد.'], ['Shop all', 'مشاهده همه'],
    ['مشاهده همه', 'Shop all'], ['فروشگاه', 'Shop'], ['بازگشت به', 'Back to'], ['بیشتر در', 'More in'],
    ['Sent to your inbox', 'ارسال به ایمیل شما'], ['Secure checkout', 'پرداخت امن'], ['Friendly support', 'پشتیبانی دوستانه'],
    ['No products match your search. Try another word.', 'محصولی با جستجوی شما پیدا نشد. واژه دیگری را امتحان کنید.'],
    ['A powerful phone for everything in your day.', 'گوشی قدرتمندی برای کارهای روزمره‌تان.'],
    ['Hear every detail in your next game.', 'هر جزئیات بازی را با صدایی شفاف بشنوید.'],
    ['A comfortable cotton essential for any day.', 'یک تی‌شرت نخی راحت برای هر روز.'],
    ['A rich, freshly roasted start to your morning.', 'صبح خود را با قهوه‌ای تازه‌برشت آغاز کنید.'],
    ['Take your music and calls anywhere.', 'موسیقی و تماس‌هایتان را همه‌جا همراه داشته باشید.'],
    ['Take control of your next match.', 'برای مسابقه بعدی کنترل بازی را به دست بگیرید.'],

    // Cart, checkout and confirmation
    ['Your cart', 'سبد خرید شما'], ['Subtotal', 'جمع جزء'], ['Taxes are calculated at checkout.', 'مالیات هنگام پرداخت محاسبه می‌شود.'],
    ['Continue shopping', 'ادامه خرید'], ['Your cart is empty', 'سبد خرید شما خالی است'],
    ['Add something from the shelf to get started.', 'برای شروع، محصولی به سبد اضافه کنید.'],
    ['Add something before checking out.', 'پیش از پرداخت، محصولی اضافه کنید.'], ['Browse products', 'مشاهده محصولات'],
    ['Open cart, ', 'باز کردن سبد خرید، '], [' item', ' مورد'], [' items', ' مورد'],
    ['Checkout', 'پرداخت'], ['Shop', 'فروشگاه'], ['Your details', 'اطلاعات شما'], ['Full name', 'نام کامل'],
    ['Your name', 'نام شما'], ['Email for delivery', 'ایمیل برای تحویل'], ['Pay with', 'روش پرداخت'],
    ['Card', 'کارت بانکی'], ['Digital wallet', 'کیف پول دیجیتال'], ['Bank transfer', 'انتقال بانکی'],
    ['This is a demo page. No payment is taken and nothing is sent.', 'این صفحه آزمایشی است. پرداختی انجام نمی‌شود و چیزی ارسال نخواهد شد.'],
    ['Place order', 'ثبت سفارش'], ['Order summary', 'خلاصه سفارش'], ['Total', 'مجموع'],
    ['Taxes are calculated when you pay. Digital items are delivered by email.', 'مالیات هنگام پرداخت محاسبه می‌شود. کالاهای دیجیتال از طریق ایمیل تحویل داده می‌شوند.'],
    ['Order confirmed', 'سفارش تأیید شد'], ['Thank you! Your keys and download links are on their way.', 'ممنون! کلیدها و لینک‌های دانلود در راه ایمیل شما هستند.'],
    ['Keep shopping', 'ادامه خرید'], ['ممنون، ', 'Thank you, '], ['! کلیدها و لینک‌های دانلود به ', '! Your keys and download links will be sent to '],
    [' ارسال می‌شود.', '.'],

    // Dock menu
    ['Support', 'پشتیبانی'], ['Sign in', 'ورود'], ['About', 'درباره ما'], ['Open support, sign in and about', 'باز کردن پشتیبانی، ورود و درباره ما'],
    ['Stuck on an activation or an order? A real person will help.', 'در فعال‌سازی یا سفارش مشکل دارید؟ یک انسان واقعی کمک می‌کند.'],
    ['Activation guide', 'راهنمای فعال‌سازی'], ['Order status', 'پیگیری سفارش'], ['Your email', 'ایمیل شما'],
    ['How can we help?', 'چطور می‌توانیم کمک کنیم؟'], ['Tell us what happened', 'بگویید چه اتفاقی افتاده'], ['Send message', 'ارسال پیام'],
    ['Your account', 'حساب شما'], ['Signed in as', 'وارد شده با'], ['Open account center', 'مرکز حساب کاربری'], ['Sign out', 'خروج'],
    ['Welcome back. Your orders and license keys are waiting.', 'خوش آمدید. سفارش‌ها و کلیدهای لایسنس شما منتظرند.'],
    ['Password', 'رمز عبور'], ['Your password', 'رمز عبور شما'],
    ['Digaily sells digital goods for everyday life: software, games, gift cards and courses. Keys and downloads land in your inbox the moment you check out.', 'دیجیلی کالاهای دیجیتال زندگی روزمره را می‌فروشد: نرم‌افزار، بازی، گیفت‌کارت و دوره. کلیدها و لینک‌های دانلود پس از خرید به ایمیل شما می‌رسند.'],
    ['Read the blog', 'خواندن وبلاگ'], ['Browse the shop', 'مرور فروشگاه'],
    ['Please enter a valid email address.', 'لطفاً یک ایمیل معتبر وارد کنید.'],
    ['Please write a short message.', 'لطفاً یک پیام کوتاه بنویسید.'], ['Thank you! We will reply to ', 'ممنون! به‌زودی به '],
    ['Please enter your password.', 'لطفاً رمز عبور را وارد کنید.'], ['Message sent.', 'پیام ارسال شد.'],

    // Account center
    ['Overview', 'نمای کلی'], ['Orders', 'سفارش‌ها'], ['Licenses & downloads', 'لایسنس‌ها و دانلودها'],
    ['Profile', 'پروفایل'], ['Security', 'امنیت'], ['Preferences', 'تنظیمات'], ['Welcome back, ', 'خوش آمدید، '],
    ['Your keys and download links are always one click away. Member since ', 'کلیدها و لینک‌های دانلود همیشه با یک کلیک در دسترس هستند. عضو از '],
    ['Open my keys', 'مشاهده کلیدها'], ['Orders placed', 'سفارش‌های ثبت‌شده'], ['Active licenses', 'لایسنس‌های فعال'],
    ['Total spent', 'مجموع خرید'], ['Items in cart', 'محصول در سبد خرید'], ['Recent orders', 'سفارش‌های اخیر'],
    ['Download my purchases', 'دانلود خریدهای من'], ['Update my profile', 'به‌روزرسانی پروفایل'], ['Secure my account', 'امن‌سازی حساب'],
    ['Everything you have bought, newest first.', 'همه خریدهای شما، از جدیدترین.'], ['No orders yet', 'هنوز سفارشی ندارید'],
    ['Your purchases will show up here.', 'خریدهای شما در اینجا نمایش داده می‌شوند.'], ['Delivered', 'تحویل‌شده'],
    ['Reorder', 'سفارش دوباره'], ['View keys', 'مشاهده کلیدها'], ['Access code', 'کد دسترسی'], ['License key', 'کلید لایسنس'],
    ['Hide', 'مخفی‌کردن'], ['Reveal', 'نمایش'], ['Copy', 'کپی'], ['Download', 'دانلود'],
    ['Reveal a key only when you need it. Keep it private.', 'کلید را فقط هنگام نیاز نمایش دهید و آن را خصوصی نگه دارید.'],
    ['Used for your receipts and key delivery.', 'برای رسیدها و تحویل کلید استفاده می‌شود.'], ['Full name', 'نام کامل'],
    ['Email', 'ایمیل'], ['Phone (optional)', 'تلفن (اختیاری)'], ['Country', 'کشور'], ['Save changes', 'ذخیره تغییرات'],
    ['Keep your account and keys safe.', 'حساب و کلیدهای خود را ایمن نگه دارید.'], ['Change password', 'تغییر رمز عبور'],
    ['Current password', 'رمز عبور فعلی'], ['New password', 'رمز عبور جدید'], ['Confirm new password', 'تأیید رمز عبور جدید'],
    ['At least 8 characters', 'حداقل ۸ نویسه'], ['Update password', 'به‌روزرسانی رمز عبور'],
    ['Two-step sign in', 'ورود دومرحله‌ای'], ['Ask for a code from your phone when you sign in.', 'هنگام ورود، کدی از تلفن شما درخواست شود.'],
    ['Signed-in devices', 'دستگاه‌های واردشده'], ['This device', 'این دستگاه'], ['Active now', 'اکنون فعال'],
    ['Last active 2 days ago', 'آخرین فعالیت: ۲ روز پیش'], ['Last active 6 days ago', 'آخرین فعالیت: ۶ روز پیش'],
    ['Sign out other devices', 'خروج از دستگاه‌های دیگر'], ['Choose what we send you. Changes save automatically.', 'انتخاب کنید چه پیام‌هایی دریافت کنید. تغییرات خودکار ذخیره می‌شوند.'],
    ['Order and delivery emails', 'ایمیل سفارش و تحویل'], ['Receipts, keys and download links.', 'رسیدها، کلیدها و لینک‌های دانلود.'],
    ['Tips and new arrivals', 'نکته‌ها و محصولات جدید'], ['Short, occasional product news.', 'خبرهای کوتاه و گاه‌به‌گاه محصولات.'],
    ['Deals and bundles', 'پیشنهادها و بسته‌ها'], ['Early access to discounts.', 'دسترسی زودهنگام به تخفیف‌ها.'],
    ['Saved.', 'ذخیره شد.'], ['Please enter a valid email address.', 'لطفاً یک ایمیل معتبر وارد کنید.'],
    ['Please enter your password.', 'لطفاً رمز عبور را وارد کنید.'], ['Please enter your name.', 'لطفاً نام خود را وارد کنید.'],
    ['Use at least 8 characters for the new password.', 'برای رمز جدید حداقل ۸ نویسه استفاده کنید.'],
    ['The two new passwords do not match.', 'دو رمز عبور جدید یکسان نیستند.'], ['Password updated.', 'رمز عبور به‌روزرسانی شد.'],
    ['Key copied.', 'کلید کپی شد.'], ['Copy failed. Reveal and copy it by hand.', 'کپی انجام نشد. کلید را نمایش دهید و دستی کپی کنید.'],
    ['Download started for ', 'دانلود آغاز شد: '], ['Other devices were signed out.', 'از دستگاه‌های دیگر خارج شدید.'],
    ['Browser on this computer', 'مرورگر این رایانه'], ['Windows · Chrome', 'ویندوز · کروم'], ['iPhone · Safari', 'آیفون · سافاری'],
    ['Friend', 'دوست'], ['United States', 'ایالات متحده'], ['Canada', 'کانادا'], ['United Kingdom', 'بریتانیا'],
    ['Germany', 'آلمان'], ['France', 'فرانسه'], ['Spain', 'اسپانیا'], ['Australia', 'استرالیا'], ['Iran', 'ایران'], ['Other', 'سایر'],

    // Language and theme accessibility
    ['Switch language to Persian', 'تغییر زبان به فارسی'], ['Switch language to English', 'تغییر زبان به انگلیسی'],
    ['Toggle dark mode', 'تغییر حالت تاریک'], ['Change to light mode', 'تغییر به حالت روشن'], ['Change to dark mode', 'تغییر به حالت تاریک'],
    ['Dark mode enabled.', 'حالت تاریک فعال شد.'], ['Light mode enabled.', 'حالت روشن فعال شد.'],
    ['Previous step', 'مرحله قبلی'], ['Next step', 'مرحله بعدی'], ['Product showcase', 'معرفی محصولات'],
    ['Explore products', 'محصولات را بررسی کنید'], ['Products', 'محصولات'], ['Details', 'جزئیات'], ['Related products', 'محصولات مرتبط'],
    ['Browse products, view details, and discover related items.', 'محصولات را مرور کنید، جزئیات را ببینید و موارد مرتبط را کشف کنید.'],
    ['Product showcase steps', 'مراحل معرفی محصولات'], ['Go to slide', 'رفتن به اسلاید'], ['Slide ', 'اسلاید '], [' از ', ' of '],
    ['$29.99', '۲۹٫۹۹ دلار'], ['Oct 28, 2026', '۶ آبان ۱۴۰۵'], ['Oct 21, 2026', '۲۹ مهر ۱۴۰۵'],
    ['Oct 14, 2026', '۲۲ مهر ۱۴۰۵'], ['Oct 7, 2026', '۱۵ مهر ۱۴۰۵'],
    ['5 min read', '۵ دقیقه مطالعه'], ['4 min read', '۴ دقیقه مطالعه'], ['3 min read', '۳ دقیقه مطالعه'],
    ['بستن پنل', 'Close panel'], ['حذف ', 'Remove '], [' از سبد خرید', ' from cart'],
    ['کاهش تعداد ', 'Decrease quantity of '], ['افزایش تعداد ', 'Increase quantity of '], ['تعداد: ', 'Qty '],
    ['به سبد خرید', 'to cart'], ['از اسلاید', 'of slide'], ['ارسال به ایمیل شما', 'Sent to your inbox'],
    ['پشتیبانی دوستانه', 'Friendly support'], ['پرداخت امن', 'Secure checkout'], ['مشاهده همه محصولات', 'View all products'],

    // Product descriptions and bullets
    ['A fast everyday smartphone with plenty of space for photos, apps and entertainment.', 'گوشی هوشمند سریع و کاربردی با فضای کافی برای عکس‌ها، برنامه‌ها و سرگرمی.'],
    ['256 GB storage and 8 GB RAM', 'حافظه ۲۵۶ گیگابایت و رم ۸ گیگابایت'], ['Sharp high-resolution display', 'نمایشگر با وضوح بالا'], ['All-day battery life', 'باتری با دوام یک‌روزه'], ['Protective case included', 'قاب محافظ همراه محصول'],
    ['Enjoy clear game audio and chat with a comfortable wireless headset built for long sessions.', 'با هدست بی‌سیم راحتی که برای بازی‌های طولانی ساخته شده، صدای بازی و گفت‌وگو را واضح بشنوید.'],
    ['Surround-sound audio', 'صدای فراگیر'], ['Detachable microphone', 'میکروفون جداشدنی'], ['Comfortable padded ear cups', 'گوشی‌های نرم و راحت'], ['Works with PC and consoles', 'سازگار با رایانه و کنسول'],
    ['A soft, everyday cotton T-shirt available in a range of sizes and colors.', 'تی‌شرت نخی نرم و روزمره در اندازه‌ها و رنگ‌های متنوع.'],
    ['Breathable cotton fabric', 'پارچه نخی و تنفس‌پذیر'], ['Comfortable regular fit', 'قالب راحت و معمولی'], ['Multiple sizes available', 'موجود در اندازه‌های مختلف'], ['Machine washable', 'قابل شست‌وشو با ماشین'],
    ['Freshly roasted espresso beans with a rich aroma and a smooth finish.', 'دانه‌های قهوه اسپرسو تازه‌برشت با عطر غنی و طعمی متعادل.'],
    ['Whole-bean coffee', 'قهوه دانه کامل'], ['Medium-dark roast', 'برشت متوسط رو به تیره'], ['Packed fresh in a resealable bag', 'بسته‌بندی تازه و زیپ‌دار'], ['Suitable for espresso machines', 'مناسب دستگاه اسپرسوساز'],
    ['Compact wireless earbuds for music, calls and listening on the go.', 'هدفون بی‌سیم کوچک برای موسیقی، تماس و استفاده روزمره.'],
    ['Noise-reduction microphones', 'میکروفون با کاهش نویز'], ['Portable charging case', 'کیس شارژ قابل‌حمل'], ['Touch controls', 'کنترل لمسی'], ['Bluetooth connection', 'اتصال بلوتوث'],
    ['A responsive wireless controller for gaming on PC or console.', 'کنترلر بی‌سیم سریع و دقیق برای بازی روی رایانه یا کنسول.'],
    ['Comfortable ergonomic grip', 'طراحی ارگونومیک و راحت'], ['Responsive buttons and triggers', 'دکمه‌ها و ماشه‌های دقیق'], ['Wireless connection', 'اتصال بی‌سیم'],
    ['A versatile smartwatch for tracking daily activity and staying connected.', 'ساعت هوشمند کاربردی برای پیگیری فعالیت روزانه و دریافت اعلان‌ها.'],
    ['Heart-rate and activity tracking', 'ردیابی ضربان قلب و فعالیت'], ['Notifications on your wrist', 'نمایش اعلان‌ها روی مچ'], ['Up to 5-day battery life', 'تا ۵ روز دوام باتری'], ['Water-resistant design', 'طراحی مقاوم در برابر آب'],
    ['A warm zip-up hoodie designed for comfortable everyday wear.', 'هودی زیپ‌دار گرم برای استفاده راحت و روزمره.'],
    ['Soft fleece lining', 'آستر نرم و گرم'], ['Full-length front zipper', 'زیپ کامل در جلو'], ['Adjustable hood', 'کلاه قابل تنظیم'],
    ['A crisp 4K monitor for work, streaming and PC gaming.', 'مانیتور 4K شفاف برای کار، تماشای فیلم و بازی رایانه‌ای.'],
    ['27-inch UHD display', 'نمایشگر UHD بیست‌وهفت اینچی'], ['Vivid color and sharp details', 'رنگ‌های زنده و جزئیات دقیق'], ['Adjustable tilt stand', 'پایه با زاویه قابل تنظیم'], ['HDMI and DisplayPort inputs', 'ورودی HDMI و DisplayPort'],
    ['Lightweight sneakers with breathable fabric and cushioning for daily walks or runs.', 'کتانی سبک با پارچه تنفس‌پذیر و زیره نرم برای پیاده‌روی یا دویدن روزانه.'],
    ['Breathable mesh upper', 'رویه مشبک و تنفس‌پذیر'], ['Cushioned insole', 'کفی نرم و راحت'], ['Durable rubber outsole', 'زیره لاستیکی بادوام'],
    ['A selection of rich dark chocolate bars for sharing or enjoying as a treat.', 'مجموعه‌ای از شکلات‌های تلخ خوش‌طعم برای پذیرایی یا میان‌وعده.'],
    ['70% cocoa chocolate', 'شکلات با ۷۰٪ کاکائو'], ['Assorted flavors', 'طعم‌های متنوع'], ['Individually wrapped bars', 'قطعه‌های بسته‌بندی‌شده'], ['Made with cocoa butter', 'تهیه‌شده با کره کاکائو'],
    ['A practical backpack with room for daily essentials, work or school.', 'کوله‌پشتی کاربردی با فضای کافی برای وسایل روزمره، کار یا مدرسه.'],
    ['20-liter capacity', 'ظرفیت ۲۰ لیتر'], ['Water-resistant outer fabric', 'رویه مقاوم در برابر آب'], ['Padded laptop compartment', 'محفظه لپ‌تاپ با لایه محافظ'], ['Adjustable shoulder straps', 'بندهای شانه قابل تنظیم'],
    ['Race through colorful tracks and compete in fast-paced console challenges.', 'در پیست‌های رنگارنگ مسابقه دهید و در چالش‌های سریع کنسولی رقابت کنید.'],
    ['Console game edition', 'نسخه بازی کنسولی'], ['Single-player and multiplayer modes', 'حالت تک‌نفره و چندنفره'], ['Unlockable cars and tracks', 'خودروها و پیست‌های قابل بازشدن'], ['Suitable for all skill levels', 'مناسب برای سطوح مختلف مهارت'],
    ['Cold-pressed extra-virgin olive oil for cooking, dressing and dipping.', 'روغن زیتون فرابکر پرس سرد برای آشپزی، سالاد و سرو کنار غذا.'],
    ['750 ml bottle', 'بطری ۷۵۰ میلی‌لیتری'], ['Cold-pressed olives', 'تهیه‌شده از زیتون پرس سرد'], ['Rich, fruity flavor', 'طعم غنی و میوه‌ای'], ['Store in a cool, dry place', 'در جای خشک و خنک نگهداری شود']
  ];

  var enToFa = Object.create(null);
  var faToEn = Object.create(null);
  pairs.forEach(function (pair) {
    enToFa[pair[0]] = pair[1];
    faToEn[pair[1]] = pair[0];
  });

  function escapeRegExp(value) {
    return value.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
  }
  function translate(value, target) {
    var result = String(value == null ? '' : value);
    var map = (target || language) === 'fa' ? enToFa : faToEn;
    var keys = Object.keys(map).sort(function (a, b) { return b.length - a.length; });
    keys.forEach(function (key) {
      result = result.replace(new RegExp(escapeRegExp(key), 'g'), map[key]);
    });
    return result;
  }

  var language = 'en';
  try {
    var saved = localStorage.getItem('digaily-language');
    if (saved === 'fa' || saved === 'en') language = saved;
  } catch (e) { }

  var textOriginals = new WeakMap();
  var renderedTexts = new WeakMap();
  var attributeOriginals = new WeakMap();
  var renderedAttributes = new WeakMap();
  var translatableAttributes = ['aria-label', 'placeholder', 'title', 'alt'];

  function shouldSkipText(node) {
    var parent = node.parentElement;
    return !parent || !!parent.closest('script,style,noscript,#languageToggle');
  }
  function processText(node) {
    if (!node || shouldSkipText(node) || !node.nodeValue.trim()) return;
    var current = node.nodeValue;
    var original = textOriginals.get(node);
    var rendered = renderedTexts.get(node);
    if (original === undefined || (rendered !== undefined && current !== rendered)) {
      original = current;
      textOriginals.set(node, original);
    }
    var next = translate(original, language);
    renderedTexts.set(node, next);
    if (node.nodeValue !== next) node.nodeValue = next;
  }
  function processAttribute(element, name) {
    if (element.id === 'languageToggle') return;
    var current = element.getAttribute(name);
    if (current == null) return;
    var saved = attributeOriginals.get(element);
    var rendered = renderedAttributes.get(element);
    if (!saved) { saved = Object.create(null); attributeOriginals.set(element, saved); }
    if (!rendered) { rendered = Object.create(null); renderedAttributes.set(element, rendered); }
    if (saved[name] === undefined || (rendered[name] !== undefined && current !== rendered[name])) saved[name] = current;
    var next = translate(saved[name], language);
    rendered[name] = next;
    if (current !== next) element.setAttribute(name, next);
  }
  function walk(node) {
    if (node.nodeType === Node.TEXT_NODE) { processText(node); return; }
    if (node.nodeType !== Node.ELEMENT_NODE && node.nodeType !== Node.DOCUMENT_NODE) return;
    if (node.nodeType === Node.ELEMENT_NODE) translatableAttributes.forEach(function (name) { processAttribute(node, name); });
    var walker = document.createTreeWalker(node, NodeFilter.SHOW_TEXT);
    var text;
    while ((text = walker.nextNode())) processText(text);
    if (node.querySelectorAll) node.querySelectorAll('*').forEach(function (element) {
      translatableAttributes.forEach(function (name) { processAttribute(element, name); });
    });
  }

  function updateToggle() {
    var button = document.getElementById('languageToggle');
    if (!button) return;
    button.textContent = language === 'en' ? 'فارسی' : 'English';
    button.setAttribute('aria-label', language === 'en' ? 'Switch language to Persian' : 'Switch language to English');
    button.setAttribute('aria-pressed', language === 'fa' ? 'true' : 'false');
  }
  function setLanguage(next, persist) {
    language = next === 'fa' ? 'fa' : 'en';
    document.documentElement.lang = language;
    document.documentElement.dir = language === 'fa' ? 'rtl' : 'ltr';
    updateToggle();
    walk(document.documentElement);
    if (persist !== false) {
      try { localStorage.setItem('digaily-language', language); } catch (e) { }
    }
    document.dispatchEvent(new CustomEvent('digaily:languagechange', { detail: { language: language } }));
  }

  var toggle = document.getElementById('languageToggle');
  if (toggle) toggle.addEventListener('click', function () { setLanguage(language === 'en' ? 'fa' : 'en'); });

  var observer = new MutationObserver(function (records) {
    records.forEach(function (record) {
      if (record.type === 'characterData') processText(record.target);
      else if (record.type === 'attributes') processAttribute(record.target, record.attributeName);
      else record.addedNodes.forEach(walk);
    });
  });
  observer.observe(document.documentElement, {
    subtree: true, childList: true, characterData: true, attributes: true,
    attributeFilter: translatableAttributes
  });

  window.DigailyLocale = {
    get language() { return language; },
    translate: translate,
    english: function (value) { return translate(value, 'en'); },
    persian: function (value) { return translate(value, 'fa'); },
    setLanguage: setLanguage
  };
  setLanguage(language, false);
})();
