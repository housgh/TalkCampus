import {appSettings} from "./appSettings.js";

class Appointments {

    
    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 1){
            localStorage.clear();
            location.href = "/html/login.html";
        }
        this.getAppointments();
    }
    
    getAppointments = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Appointment`,
            method: "GET",
            success: this.updateAppointmentsView,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
    
    updateAppointmentsView = (data) => {
        data.today.forEach(appointment => {
            this.appendAppointmentElement( appointment, "today");
        });

        data.tomorrow.forEach(appointment => {
            this.appendAppointmentElement(appointment, "tomorrow");
        });

        data.thisWeek.forEach(appointment => {
            this.appendAppointmentElement(appointment, "thisWeek");
        });
    }
    

    appendAppointmentElement = (appointment, parentElementId) => {
        let inputGroup = document.createElement("div");
        inputGroup.className = "input-group";
        inputGroup.style = "margin-top: 1%";
        let input = document.createElement("input");
        input.type = "text";
        input.className = "form-control";
        input.disabled = true;
        var startDate = new Date(appointment.startDate);
        var endDate = new Date(appointment.endDate);
        var dateString = `${startDate.getDate()}/${startDate.getMonth()+1}/${startDate.getFullYear()}`
        var startTime = `${startDate.getHours()}:${startDate.getMinutes() > 10 ? startDate.getMinutes() : "00"}`
        var endTime = `${endDate.getHours()}:${startDate.getMinutes() > 10 ? startDate.getMinutes() : "00"}`
        input.value = `${dateString} ${startTime} to ${endTime}`;
        inputGroup.append(input);
        if (new Date(appointment.startDate).getDate() === new Date().getDate() && 
            new Date(appointment.startDate).getHours() <= new Date().getHours() && 
            (new Date(appointment.startDate).getHours() - new Date().getHours()) <= 1) {
            let joinButton = document.createElement("button");
            joinButton.className = "btn btn-success";
            joinButton.type = "button";
            joinButton.innerText = "Join";
            joinButton.onclick = () => this.onJoinClick(appointment.id);
            inputGroup.append(joinButton);
        }
        let declineButton = document.createElement("button");
        declineButton.className = "btn btn-danger";
        declineButton.type = "button";
        declineButton.innerText = "Decline";
        declineButton.onclick = () => this.declineAppointment(appointment.id)
        inputGroup.append(declineButton);
        $(`#${parentElementId}`).append(inputGroup);
    }

    declineAppointment = (id) => {
        $.ajax({
            url: `${appSettings.apiUrl}Appointment/Decline/${id}`,
            method: "PUT",
            success: location.reload(),
            headers: {userId: localStorage.getItem("userId")}
        });
    }

    onJoinClick = (id) => {
        location.href = `/html/chat.html?chatId=${id}`;
    }
}

let appointmentsModel;

$(document).ready(() => {
    appointmentsModel = new Appointments();
})