import {appSettings} from "./appSettings.js";

class Schedule{
    constructor() {
        var userId = localStorage.getItem("userId");
        var roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 1) location.href = "/html/login";
        this.getSchedule();
    }
    
    getSchedule = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Schedule`,
            method: 'GET',
            success: this.onGetScheduleSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }

    onGetScheduleSuccess = (data) => {
        console.log(data)
        data.scheduleDays.forEach(e =>
        {
            let updatedStartHour = e.startHour < 9 ? e.startHour + 12 : e.startHour;
            let updatedEndHour = e.endHour < 9 ? e.endHour + 12 : e.endHour;
            let emptyCellsCount = updatedStartHour - 9;
            let cellsCount = Math.abs(updatedEndHour - updatedStartHour);
            let emptyCells = document.createElement("div");
            emptyCells.className = `row-cell-${emptyCellsCount + 1}`;
            let cells = document.createElement("div");
            cells.className = `row-cell-${cellsCount + 1} active-cell`;
            cells.innerText = `${e.startHour}:00 to ${e.endHour}:00`;
            $(`#day-${e.dayOfWeekId}`).append(emptyCells);
            $(`#day-${e.dayOfWeekId}`).append(cells);
        });
    }
    
    declineAppointment = (id) => {
        $.ajax({
            url: `${appSettings.apiUrl}Appointment/Decline/${id}`,
            method: 'PUT',
            success: this.onGetScheduleSuccess,
            headers: {userId: localStorage.getItem("userId")}
        });
    }
}

let scheduleModule;

$(document).ready(() => {
    scheduleModule = new Schedule();
})