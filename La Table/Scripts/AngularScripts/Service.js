app.service("LaTableService", function ($http) {

    // CHECK IF EMAIL EXISTS
    this.checkEmailExists = function (email) {
        return $http.get("/Home/CheckEmailExists", { params: { email: email } });
    };

    // REGISTER
    this.registerUser = function (user) {
        return $http.post("/Home/RegisterUser", user);
    };

    // LOGIN
    this.loginUser = function (credentials) {
        return $http.post("/Home/Login", credentials);
    };

    // LIST ACCOUNTS
    this.getUsers = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetUsers'
        });
    };

    // EDIT - UPDATE ACCOUNT
    this.updateUser = function (user) {
        return $http.post('/Home/UpdateUser', user);  
    };

    this.createLogEntry = function (log) {
        return $http.post('/Home/CreateLogEntry', log); 
    };


    // LIST LOGS
    this.getLogs = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetLogs'
        });
    };

 
});