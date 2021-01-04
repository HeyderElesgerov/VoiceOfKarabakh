// Donation box scroll position

function donationScroll () {
    let spotlight2 = document.querySelector('.spotlight-2');
    let donationBox = document.querySelector('.donation-box');

    window.addEventListener('scroll',()=>{
        if(spotlight2.getBoundingClientRect().top<182){
            if(!donationBox.classList.contains('donation-absolute')){
                donationBox.classList.add('donation-absolute');
            }
        }
        else{
            if(donationBox.classList.contains('donation-absolute')){
                
                donationBox.classList.remove('donation-absolute');
            }
            
        }
    })
};

donationScroll();


//Email add or removal

function emailEnterMode () {
    let emailBtn = document.querySelector('.email-btn');
    let removeEmail = document.querySelector('.email-removal');

    removeEmail.addEventListener('click',()=>{
        emailBtn.classList.toggle('removal-mode');
        if(emailBtn.classList.contains('removal-mode')){
            removeEmail.innerText = 'Join the newsletter';
            emailBtn.innerText = 'Remove Your E-mail'
        }
        else{
            removeEmail.innerText = 'Remove your e-mail';
            emailBtn.innerText = 'Join The Newsletter';
        }
    });
}

emailEnterMode();


//Autoplay slides

function autoSlides () {
    firstSpotlight = document.querySelector('.spotlight-1');
    secondSpotlight = document.querySelector('.spotlight-2');
    itemsInside = firstSpotlight.querySelectorAll('.item');
    itemsInside2 = secondSpotlight.querySelectorAll('.item');
    
    slidecount = itemsInside.length;
    
    for(let i=0;i<slidecount;i++){
        itemsInside[i].style.backgroundImage = `url("/assets/sliderbg/pic${i+1}.jpg")`;
        itemsInside2[i].style.backgroundImage = `url("/assets/sliderbg/pic${i+6}.jpg")`;
    }
    
    itemsInside[0].classList.add('active');
    itemsInside2[0].classList.add('active');
    
    slideLoop(firstSpotlight);
    slideLoop(secondSpotlight);
}

autoSlides();


function slideLoop (container) {
    setTimeout(() => {
        
    }, 2000);
    
    setInterval(() => {
        let activeOne = container.querySelector('.active');
        let nextOne = activeOne.nextElementSibling ? activeOne.nextElementSibling : activeOne.parentElement.firstElementChild;
        activeOne.classList.add('going');
        nextOne.classList.add('coming');
        setTimeout(()=>{
            activeOne.classList.remove('active');
            nextOne.classList.add('active');
            activeOne.classList.remove('going');
            nextOne.classList.remove('coming');
        },500);
    }, 5000);
}
