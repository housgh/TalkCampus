class Index{
    constructor() {
        let userId = localStorage.getItem("userId");
        let roleId = localStorage.getItem("roleId");
        
        if(!userId || !roleId){
            localStorage.clear();
            location.href = "/html/login.html";
        }
        console.log(roleId)
        let html = roleId == 1? "/html/psychologist-dashboard.html" : roleId == 2? "/html/patient-dashboard.html" : "/html/users.html";
        $("body").load(html);
    }
}

new Index();