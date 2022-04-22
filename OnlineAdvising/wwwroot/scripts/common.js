class Common{
    constructor() {
        $("#header").load("../html/header.html");
    }
}

let commonModule;

$(document).ready(() => {
    commonModule = new Common();
})