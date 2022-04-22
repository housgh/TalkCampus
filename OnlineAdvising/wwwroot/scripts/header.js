class Header{
    constructor() {
        let roleId = localStorage.getItem("roleId");
        if(!roleId){
            localStorage.clear();
            location.href = "/html/login.html";
        }
        if(roleId == 2){
            $("#appointments").css("display","none");
            $("#schedule").css("display","none");
        }
        this.bindEvents();
    }
    
    bindEvents = () => {
        $("#logout").click(this.logout);
        $("#home").click(() => location.href ="/html/index.html");
        $("#appointments").click(() => location.href = "/html/appointments.html");
        $("#schedule").click(() => location.href = "/html/schedule.html");
        $("#archive").click(() => location.href = "/html/archives.html");
    }
    
    logout = () => {
        localStorage.clear();
        location.href = "/html/login.html";   
    }
}

let headerModule;

$(document).ready(() => {
    headerModule = new Header();
})