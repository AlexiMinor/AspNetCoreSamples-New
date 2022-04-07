let userName = 'UserName';

let specificFunction = function() {
    console.log('sf');
    return 15;
};

function showName(role = specificFunction(), text = 'Hello', specificFunc = specificFunction) {
    let userName = 'Vasya';
    console.log(`${text} ${role} ${userName}`);
    specificFunc();
};

function multiplyX2(a) {
    a = a + a;
    return a;
}

function changeTextColor() {
    var paragraphs = document.getElementsByTagName('p');
    for (var i = 0; i < paragraphs.length; i++) {
        debugger;
        paragraphs[i].style.color = '#000';
    }
};

function changeFontColor(color) {
    var paragraphs = document.getElementsByTagName('p');
    for (var i = 0; i < paragraphs.length; i++) {
        paragraphs[i].style.color = color;
    }

    let content = document.getElementsByClassName('text-center');
    let p = document.createElement("p");
    p.innerText = 'Created p';


    p.addEventListener("click", changeTextColor);
    content[0].appendChild(p);
};

function makeBold(event) {
    let target = event.target;

    target.style.fontWeight = 800;
};


function makeNormal(event) {
    let target = event.target;

    target.style.fontWeight = 200;
};
//showName();

//let age = prompt('How many years?', 100);
//console.log(age);

//let isAdmin = confirm('Are you admin?');
//console.log(isAdmin);



let a = 1;

let b = multiplyX2(a);

console.log(a);
console.log(b);

let welcomeMsg = document.getElementById('welcome-message');

//let isConfirmed = confirm('Do you want to change welcome message?');

//if (isConfirmed) {
//    welcomeMsg.innerHTML = 'Hello World';
//}

let paragraph = document.getElementsByClassName('custom-element')[0];
console.log(paragraph);

//welcomeMsg.addEventListener("mouseenter", makeBold);
//welcomeMsg.addEventListener("mouseleave", makeNormal);


