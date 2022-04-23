import {appSettings, daysOfWeek} from "./appSettings.js";

class Schedule{
    constructor() {
        var userId = localStorage.getItem("userId");
        var roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 1) location.href = "/html/login";
        this.getSchedule();
        this.bindActions();
    }
    
    bindActions = () => {
        $("#create").click(this.createSchedule);
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
        if(!data.scheduleDays || data.length <= 0){
            $("#createScheduleModal").modal({
                backdrop: "static"
            });
            return;
        }
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
    
    createSchedule = () => {
        let schedule = {
            scheduleDays: []
        };
        daysOfWeek.forEach((d, i) => {
            let from = $(`#${d}-from`).val();
            let to = $(`#${d}-to`).val();
            console.log(from + "-" + to);
            if(!from && !to){
                return;
            }
            from = from?? 9;
            to = to?? 6;
            if((from < 9 && from > 5) || (from > 12) || (from < 1)){
                alert("start hour must be between 9 and 12 or between 1 and 5");
                return;
            }
            if((to < 10 && to > 6) || (to > 12) || (to < 1)){
                alert("end hour must be between 10 and 12 or between 1 and 6");
                return;
            }
            from = from > 12? from + 12 : from;
            to = to > 12? to + 12 : to;
            schedule.scheduleDays.push({
                dayOfWeekId: i,
                startHour: from,
                endHour: to
            });
        });
        $.ajax({
            url: `${appSettings.apiUrl}Schedule`,
            method: 'POST',
            data: JSON.stringify(schedule),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: () => location.reload(),
            headers: {userId: localStorage.getItem("userId")}
        });
    }
}

let scheduleModule;

$(document).ready(() => {
    scheduleModule = new Schedule();
})