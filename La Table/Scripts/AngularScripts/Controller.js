app.controller("LaTableController", function ($scope, LaTableService) {

    // SIGNUP
    $scope.submitFunc = function () {

        // required
        if ($scope.fName == null) {
            Swal.fire({ title: "Error", text: "Please enter your First Name", icon: "error" });
        }
        else if ($scope.fName.length > 30) {
            Swal.fire("Error", "First Name must not exceed 30 characters", "error");
        }
        else if ($scope.mName && $scope.mName.length >= 30) {
            Swal.fire({ title: "Error", text: "Middle Name must not exceed 30 characters", icon: "error" });
        }
        else if ($scope.lName == null) {
            Swal.fire({ title: "Error", text: "Please enter your Last Name", icon: "error" });
        }
        else if ($scope.lName.length > 30) {
            Swal.fire("Error", "Last Name must not exceed 30 characters", "error");
        }
        else if ($scope.uEmail == null) {
            Swal.fire({ title: "Error", text: "Please enter your Email", icon: "error" });
        }

        // email format
        else if (!/\S+@\S+\.\S+/.test($scope.uEmail)) {
            Swal.fire("Error", "Invalid email format", "error");
        }

        else if ($scope.uPhone == null) {
            Swal.fire({ title: "Error", text: "Please enter your Phone Number", icon: "error" });
        }

        // phone number 11 digits
        else if (!/^\d{11}$/.test($scope.uPhone)) {
            Swal.fire("Error", "Phone number must be 11 digits", "error");
        }

        else if ($scope.uPass == null || $scope.uPass.length < 8) {
            Swal.fire({ title: "Error", text: "Password must be at least 8 characters long", icon: "error" });
        }
        else if (
            !/[A-Z]/.test($scope.uPass) ||    // 1 uppercase letter
            !/[a-z]/.test($scope.uPass) ||    // 1 lowercase letter
            !/[0-9]/.test($scope.uPass) ||    // 1 number
            !/[!@#$%^&*(),.?":{}|<>]/.test($scope.uPass)) {    // 1 special character
            Swal.fire({
                title: "Error",
                text: "Password must include an uppercase letter, a lowercase letter, a number, and a special character",
                icon: "error"
            });
        }
        else if ($scope.uCPass == null) {
            Swal.fire({ title: "Error", text: "Please confirm your Password", icon: "error" });
        }

        // matching passwords
        else if ($scope.uPass !== $scope.uCPass) {
            Swal.fire("Error", "Passwords do not match", "error");

        }

        // email already exists
        else {
            LaTableService.checkEmailExists($scope.uEmail).then(function (response) {
                if (response.data.exists) {
                    Swal.fire("Error", "Email is already registered!", "error");
                } else {
                    // REGISTER
                    var newUser = {
                        firstName: $scope.fName,
                        midName: $scope.mName,
                        lastName: $scope.lName,
                        Phone_Num: $scope.uPhone,
                        email: $scope.uEmail,
                        password: $scope.uPass,
                        RoleID: 3,  // 3 = customer
                        StatusID: 2, // 2 = inactive
                        createAt: new Date(),
                        updateAt: new Date()
                    };

                    LaTableService.registerUser(newUser).then(function () {
                        Swal.fire("Success", "Account created successfully!", "success").then(() => {
                            window.location.href = "/Home/Login";
                        });
                    })
                }
            })
        }
    }
    
    // SIGNUP CANCEL
    $scope.cancelFunc = function () {
        $scope.fName = null;
        $scope.mName = null;
        $scope.lName = null;
        $scope.uAddress = null;
        $scope.uEmail = null;
        $scope.uPhone = null;
        $scope.uPass = null;
        $scope.uCPass = null;
    }

    // LOGIN
    $scope.loginFunc = function () {
        if (!$scope.email || !$scope.password) { 
            Swal.fire("Error", "Both fields are required", "error");
        } else {
            var credentials = {
                email: $scope.email,
                password: $scope.password
            };
            LaTableService.loginUser(credentials).then(function (response) {
                if (response.data.success) {
                    // SUCCESSFUL LOGIN, REDIRECT BASED ON ROLE
                    Swal.fire("Success", response.data.message, "success").then(() => {
                        window.location.href = response.data.redirectUrl; 
                    });
                } else {
                    Swal.fire("Error", response.data.message, "error");
                }
            }).catch(function (error) {
                Swal.fire("Error", "An error occurred while processing your login. Please try again.", "error");
            });
        }
    };

    // LIST ACCOUNTS
    $scope.loadUsers = function () {
        LaTableService.getUsers().then(function (response) {
            if (response.data.success) {
                $scope.users = response.data.data;
                initializeDataTable();

            }
             else {
                console.error('Failed to load users:', response.data.message);
            }
        }, function (error) {
            console.error('Error loading users:', error);
        });

    };

    function initializeDataTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#accountsTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 5,
                    language: {
                        search: "Find Accounts:",
                    },
                    columns: [
                        { title: "Account ID" },
                        { title: "First Name" },
                        { title: "Last Name" },
                        { title: "Phone Number" },
                        { title: "Email" },
                        { title: "Role" },
                        { title: "Status" },
                        { title: "Actions" },

                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    // EDIT USER
    $scope.editUser = function (account) {
        if (!account) {
            console.error('No user provided to editUser function');
            return;
        }
        console.log('Editing User:', account);

        $scope.editAccountID = account.AccountID;  
        $scope.editFirstName = account.firstName;  
        $scope.editLastName = account.lastName;      
        $scope.editEmail = account.email;            

        // OPEN EDIT MODAL
        $('#editModal').modal('open');
    };

    // UPDATE USER
    $scope.updateUser = function () {
        const updatedAccount = {
            AccountID: $scope.editAccountID, 
            firstName: $scope.editFirstName,  
            lastName: $scope.editLastName,   
            email: $scope.editEmail,         
        };

        // CONFIRMATION
        Swal.fire({
            title: 'Are you sure you want to save your changes?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, save it!'
        }).then((result) => {
            if (result.isConfirmed) {

                LaTableService.updateUser(updatedAccount).then(function (response) {
                    if (response.data.success) {
                        var log = {
                            AccountID: updatedAccount.AccountID,
                            action: "Updated", 
                            timestamp: new Date()  
                        };
                        LaTableService.createLogEntry(log).then(function (logResponse) {
                            if (logResponse.data.success) {
                                Swal.fire({
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'User Updated!',
                                });
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Failed to log the action.',
                                });
                            }
                        });

                        $scope.loadUsers(); 
                        $('#editModal').modal('close');
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: response.data.message,
                        });
                    }
                }, function (error) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Could not update account: ' + error.data.message,
                    });
                });
            }
        });
    };

    // CLOSE EDIT MODAL
    $scope.closeModal = function () {
        $('#editModal').modal('close');
    };

    // LIST LOGS
    $scope.loadLogs = function () {
        LaTableService.getLogs().then(function (response) {
            if (response.data.success) {
                // FORMAT LOGS DATA TIMESTAMP ex: December 23, 2024 2:30PM
                $scope.logs = response.data.data.map(log => ({
                    LogsID: log.LogsID,
                    UserFullName: log.UserFullName,
                    action: log.action,
                    timestamp: new Date(parseInt(log.timestamp.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    })
                }));
                // DESCENDING ORDER
                $scope.logs.sort((a, b) => b.LogsID - a.LogsID);

                console.log('Logs Loaded:', $scope.logs);

                initializeLogsTable();
            } else {
                console.error('Failed to load logs:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading logs:', error);
        });
    };

    function initializeLogsTable() {
            if (typeof $ !== 'undefined' && $.fn.DataTable) {
                setTimeout(function () {
                    $('#logsTable').DataTable({
                        destroy: true, 
                        paging: true,
                        searching: true,
                        ordering: true,
                        responsive: true,
                        autoWidth: false,
                        fixedHeader: true, 
                        language: {
                            search: "Find logs:",
                        },
                        columns: [
                            { title: "Log ID" },
                            { title: "User Full Name" },
                            { title: "Action" },
                            { title: "Timestamp" },
                        ],
                    })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

});