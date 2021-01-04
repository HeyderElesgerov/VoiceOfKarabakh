//Changing the language

let langOpt = document.querySelector('.lang-options');
let langList = document.querySelector('.lang-list');
let langs = document.querySelectorAll('.lang');

langOpt.addEventListener('mouseenter',(e)=>{
    langList.classList.add('active');
    e.target.addEventListener('mouseleave',()=>{
        langList.classList.remove('active');
    })
});

langOpt.addEventListener('click',()=>{
    langList.classList.add('active');
})

langs.forEach(lang=>{
    lang.addEventListener('click',()=>{
        console.log("working biyatch")
        langList.classList.remove('active');
    });
});


//Hamburger Menu toggle

let burgerBtn = document.querySelector('.toggle-btn');
let navigation = document.querySelector('nav');
let navLinks = navigation.querySelectorAll('a');

burgerBtn.addEventListener('click',()=>{
    burgerBtn.classList.toggle('toggle-close');
    navigation.classList.toggle('nav-active');
    navLinks.forEach(navLink=>{
        navLink.addEventListener('click',()=>{
            burgerBtn.classList.remove('toggle-close');
            navigation.classList.remove('nav-active');
        });
    });
});
