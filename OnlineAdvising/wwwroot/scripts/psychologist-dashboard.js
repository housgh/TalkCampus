import {appSettings} from "./appSettings.js";

class PsychologistDashboard {
    
    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        if(!userId || !roleId || roleId != 1) location.href = "/html/login.html";
        this.getDashboardData();
    }
    
    getDashboardData = () => {
        $.ajax({
            url: `${appSettings.apiUrl}Dashboard/Psychologist`,
            method: "GET",
            success: this.updateDashboard,
            headers: {userId: localStorage.getItem("userId")}
        })
    }

    updateDashboard = (data) => {
        console.log(data)
        if(data.accountStatusId == 3){
            localStorage.clear();
            alert("Your account is still under review, try again later.");
            location.href = "/html/login.html";
        }
        $("#name").text("\t" + data.fullName);
        $("#email").text("\t" + (data.email));
        $("#biography").text("\t" + data.biography);
        $("#hoursServed").text(data.hoursServed);
        $("#studentsHelped").text(data.studentsHelped);
        this.updateRatingStars(data.averageRating);
    }

    updateRatingStars = (rating) => {
        if(rating){
            let temp = Math.floor(rating);
            for (let i = 0; i < temp; i++) {
                var star = document.createElement("i");
                star.className = "fas fa-star";
                $("#rating").append(star);
                rating--;
            }
            console.log(rating)
            if (rating > 0) {
                var star = document.createElement("i");
                star.className = rating < 1 ? "fas fa-star-half-alt" : "fas fa-star";
                $("#rating").append(star);
            }
            return;
        }
        $("#rating").html("<h3>No Rating Yet!</h3>");
        $("#ratingTitle").html("");
    }
}

let dashboardModule;

$(document).ready(() => {
    dashboardModule = new PsychologistDashboard();
})
