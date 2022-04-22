import {appSettings} from "./appSettings.js";

class PatientDashboard {
    
    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 2) location.href = "/html/login.html";
        this.getDashboardData();
        this.bindEvents();
    }
    
    bindEvents = () => {
        $("#create").on("click", () => this.createAppointment());
    }

    getDashboardData = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Dashboard/Patient`,
            method: "GET",
            success: this.updateDashboard,
            headers: {userId: localStorage.getItem("userId")}
        })
    }

    updateDashboard = (data) => {
        $("#name").text("\t" + data.name);
        $("#email").text("\t" + (data.email));
        if(data.hasAppointment){
            $("#reserve").css("display", "none");
            $("#join").on("click", () => location.href = `/html/chat.html?chatId=${data.upcomingChatId}`);
            return;
        }
        $("#join").css("display", "none");
    }
    
    createAppointment = () => {
        var date = $("#date").val();
        var hour = $("#hour").val();
        var model = {
            startDate: new Date(`${date} ${(hour - (new Date().getTimezoneOffset()/60))}:00:00`)
        }
        $.ajax({
            url: `${appSettings.apiUrl}Appointment`,
            method: "POST",
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: this.onCreateAppointmentSuccess,
            headers: {userId: localStorage.getItem("userId")}
        })
    }

    onCreateAppointmentSuccess = () => {
        $('#appointmentModal').modal('hide');
        alert("appointment created!");
    }
}

let dashboardModule;

$(document).ready(() => {
    dashboardModule = new PatientDashboard();
})
