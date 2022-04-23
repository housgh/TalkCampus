import {appSettings} from "./appSettings.js";

class Login {

    constructor() {
        this.bindActions();
    }

    bindActions = () => {
        $('.message a').click(this.toggleForm);
        $("#login").click(this.login);
        $("#register").click(this.onRegisterClick);
        $("#continue").click(this.register);
    }
    toggleForm = () => {
        $('form').animate({
            height: "toggle",
            opacity: "toggle"
        }, "slow");
    }

    login = () => {
        let model = {
            username: $("#username").val(),
            password: $("#password").val()
        }
        if(!model.username || !model.password){
            alert("please fill all fields before proceeding.");
            return;
        }
        $.ajax({
            url: `${appSettings.apiUrl}Authentication/login`,
            method: "POST",
            data: JSON.stringify(model),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: this.onLoginSuccess
        });
    }

    onRegisterClick = () => {
        let roleId = $("#register_role").val();
        let firstname = $("#register_role").val();
        let lastname = $("#register_role").val();
        let password = $("#register_role").val();
        let email = $("#register_role").val();

        if (!roleId || !firstname || !lastname || !password || !email) {
            alert("please fill all fields before proceeding.");
            return;
        }

        if (roleId == 1) {
            $("#registerModal").modal('toggle');
            return;
        }
        this.register();
    }

    register = () => {
        let formData = new FormData();
        formData.append("firstName", $("#register_firstname").val());
        formData.append("lastName",  $("#register_lastname").val());
        formData.append("username", $("#register_username").val());
        formData.append("email", $("#register_email").val());
        formData.append("password", $("#register_password").val());
        formData.append("roleId", $("#register_role").val());

        if (formData.get("roleId") == 1 && !($("#biography").val() && $("#degree").val())) {
            alert("please fill all fields before proceeding.");
            return;
        }
        else if(formData.get("roleId") == 1 ){
            let files = document.getElementById("files").files;
            for(let i =0;i<files.length;i++){
                formData.append(`file_${i}`, files[i]);
            }
            formData.append("biography", $("#biography").val());
            formData.append("degreeName", $("#degree").val());
        }

        $.ajax({
            url: `${appSettings.apiUrl}Authentication/register`,
            method: "POST",
            dataType: 'json',
            processData: false,
            contentType: false,
            data: formData,
            error: (data) => alert(data.responseText),
            success: this.onLoginSuccess
        });
    }

    onLoginSuccess = (result) => {
        console.log(result);
        localStorage.setItem("userId", result.id);
        localStorage.setItem("roleId", result.roleId);
        location.href = result.roleId == 3? "/html/users.html" : "/html/index.html";
    }
}

let loginModule;

$(document).ready(() => {
    loginModule = new Login();
})