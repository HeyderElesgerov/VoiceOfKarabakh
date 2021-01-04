let thumb = document.querySelector('.thumb');
let tagname = document.querySelector('.tagname');
let date = document.querySelector('.date');
let title = document.querySelector('h2');
let content = document.querySelector('.content');
let posId = document.getElementById('id').value;

fetch(`/api/newsposts/${posId}`)
.then(response=>response.json())
.then(data=>{
    thumb.style.backgroundImage = `url("${data.photoPath}")`;
    title.innerText = `${data.title}`;
    tagname.innerText = `#${data.tags[0].tagTitle.toLowerCase()}`;
    date.innerText = `${convertDateFormat(data.createdDate)}` || "Date not defined";
    content.innerHTML = data.content;
});


function convertDateFormat (date) {
    let month = date.slice(5,7);
    let day = date.slice(8-10);
    let monthName = 'Jan';
    switch (month) {
        case '01':
            monthName = 'Jan';
            break;
        case '02':
            monthName = 'Feb';
            break;
        case '03':
            monthName = 'Mar';
            break;
        case '04':
            monthName = 'Apr';
            break;
        case '05':
            monthName = 'May';
            break;
        case '06':
            monthName = 'Jun';
            break;
        case '07':
            monthName = 'Jul';
            break;
        case '08':
            monthName = 'Aug';
            break;
        case '09':
            monthName = 'Sep';
            break;
        case '10':
            monthName = 'Oct';
            break;
        case '11':
            monthName = 'Now';
            break;
        case '12':
            monthName = 'Dec';
            break;
    }
    return monthName + ' ' + day;
}