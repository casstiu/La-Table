app.controller("LaTableController", function ($scope, $http, LaTableService) {

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
                        StatusID: 1,
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
                $scope.$applyAsync(() => {
                    initializeAccountTable();  
                });
            } else {
                console.error('Failed to load users:', response.data.message);
            }
        }, function (error) {
            console.error('Error loading users:', error);
        });
    };

    function initializeAccountTable() {
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
                    pageLength: 6,
                    language: {
                        search: "Find Account:",
                        lengthMenu: "",
                    },
                    columns: [
                        { title: "Account ID" },
                        { title: "First Name" },
                        { title: "Last Name" },
                        { title: "Phone Number" },
                        { title: "Email" },
                        { title: "Role" },
                        { title: "Actions", orderable: false },
                    ],
                    initComplete: function () {
                        $('.dataTables_filter').css({
                            'text-align': 'left',
                            'display': 'flex',
                            'align-items': 'center',
                            'gap': '15px',
                            'padding-left': '10px',
                        });

                        $('.dataTables_filter label').css({
                            'font-size': '16px',
                            'color': '#472F35',
                            'font-weight': 'bold',
                            'margin-right': '10px',
                            'margin-bottom': '0',
                        });

                        $('.dataTables_filter input[type="search"]').css({
                            'border': '1px solid #AD9162',
                            'padding': '10px',
                            'background-color': '#F1EDDA',
                            'color': '#472F35',
                            'border-radius': '30px',
                            'width': '300px',
                            'box-sizing': 'border-box',
                        });
                    },
                });
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    // ADD ACCOUNT
    $scope.submitAddAccount = function () {
        if ($scope.fName == null || $scope.fName.length > 30) {
            Swal.fire("Error", "First Name must not exceed 30 characters", "error");
            return;
        }
        if ($scope.lName == null || $scope.lName.length > 30) {
            Swal.fire("Error", "Last Name must not exceed 30 characters", "error");
            return;
        }
        if (!$scope.uEmail || !/\S+@\S+\.\S+/.test($scope.uEmail)) {
            Swal.fire("Error", "Invalid email format", "error");
            return;
        }
        if ($scope.uPhone == null || !/^\d{11}$/.test($scope.uPhone)) {
            Swal.fire("Error", "Phone number must be 11 digits", "error");
            return;
        }
        if ($scope.uPass == null || $scope.uPass.length < 8 || !/[A-Z]/.test($scope.uPass) || !/[a-z]/.test($scope.uPass) || !/[0-9]/.test($scope.uPass) || !/[!@#$%^&*(),.?":{}|<>]/.test($scope.uPass)) {
            Swal.fire("Error", "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character", "error");
            return;
        }

        LaTableService.checkEmailExists($scope.uEmail).then(function (response) {
            if (response.data.exists) {
                Swal.fire("Error", "Email is already registered!", "error");
            } else {
                var newUser = {
                    firstName: $scope.fName,
                    lastName: $scope.lName,
                    Phone_Num: $scope.uPhone,
                    email: $scope.uEmail,
                    password: $scope.uPass,
                    RoleID: $scope.Role,
                    StatusID: 1,
                    createAt: new Date(),
                    updateAt: new Date()
                };

                LaTableService.addAccount(newUser).then(function (response) {
                    if (response.data.success) {
                        Swal.fire("Success", "Account created successfully!", "success").then(function () {
                            window.location.reload();
                        });
                    } else {
                        Swal.fire("Error", response.data.message, "error");
                    }
                });
            }
        });
    };

    // EDIT ACCOUNT
    $scope.editAcc = function (account) {
        $scope.editAccID = account.accountID;
        $scope.editFirstName = account.firstName;
        $scope.editLastName = account.lastName;
        $scope.editPhone = account.Phone_Num;
        $scope.editEmail = account.email;

        const modalElement = document.getElementById('editModal');
        const modalInstance = M.Modal.init(modalElement);
        modalInstance.open();

        setTimeout(() => {
            M.updateTextFields();
        }, 0);
    }
    $scope.updateAcc = function () {
        var editAccID = $scope.editAccID;
        var editFirstName = $scope.editFirstName;
        var editLastName = $scope.editLastName;
        var editPhone = $scope.editPhone;
        var editEmail = $scope.editEmail;

        var postData = LaTableService.updateAcc(editAccID, editFirstName, editLastName, editPhone, editEmail);

        window.location.reload();
    }

    // CLOSE EDIT MODAL
    $scope.closeModal = function () {
        $('#editModal').modal('close');
    };

    // DEACTIVATE ACCOUNT
    $scope.deleteUser = function (accID) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
        }).then((result) => {
            if (result.isConfirmed) {
                LaTableService.deleteUser(accID).then(function (response) {
                    if (response.data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: response.data.message,
                        });
                        window.location.reload();
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
                        text: 'An error occurred: ' + error.data.message,
                    });
                });
            }
        });
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
                        search: "Find Log:",
                        lengthMenu: "",
                    },
                    columns: [
                        { title: "Log ID" },
                        { title: "User Full Name" },
                        { title: "Action" },
                        { title: "Timestamp" },
                    ],
                    initComplete: function () {
                        $('.dataTables_filter').css({
                            'text-align': 'left',
                            'display': 'flex',
                            'align-items': 'center',
                            'gap': '10px',
                            'padding-left': '30px',
                        });

                        $('.dataTables_filter label').css({
                            'font-size': '16px',
                            'color': '#472F35',
                            'font-weight': 'bold',
                            'margin-right': '10px',
                            'margin-bottom': '0',
                        });

                        $('.dataTables_filter input[type="search"]').css({
                            'border': '1px solid #AD9162',
                            'padding': '10px',
                            'background-color': '#F1EDDA',
                            'color': '#472F35',
                            'border-radius': '30px',
                            'width': '300px',
                            'box-sizing': 'border-box',
                        });
                    }
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    // LIST PROMOS
    $scope.loadPromos = function () {
        LaTableService.getPromos().then(function (response) {
            if (response.data.success) {
                $scope.promos = response.data.data.map(promo => ({
                    promoID: promo.PromoID,
                    image: promo.promoImage ? promo.promoImage.replace("~", "") : "/path/to/default/image.jpg", // Handle image path
                    name: promo.promoName,
                    description: promo.description,
                    start_date: new Date(parseInt(promo.start_date.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    end_date: new Date(parseInt(promo.end_date.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initializePromoTable();

            } else {
                console.error('Failed to load promos:', response.data.message);
            }
        }, function (error) {
            console.error('Error loading promos:', error);
        });
    };

    function initializePromoTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#promosTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 5,
                    language: {
                        search: "Find Promo:",
                        lengthMenu: "",
                    },
                    columns: [
                        { title: "Promo ID" },
                        { title: "Promo Image" },
                        { title: "Promo Name" },
                        { title: "Description" },
                        { title: "Start Date" },
                        { title: "End Date" },
                        { title: "Actions", orderable: false },
                    ],
                    initComplete: function () {
                        $('.dataTables_filter').css({
                            'text-align': 'left',
                            'display': 'flex',
                            'align-items': 'center',
                            'gap': '15px',
                            'padding-left': '10px',
                        });

                        $('.dataTables_filter label').css({
                            'font-size': '16px',
                            'color': '#472F35',
                            'font-weight': 'bold',
                            'margin-right': '10px',
                            'margin-bottom': '0',
                        });

                        $('.dataTables_filter input[type="search"]').css({
                            'border': '1px solid #AD9162',
                            'padding': '10px',
                            'background-color': '#F1EDDA',
                            'color': '#472F35',
                            'border-radius': '30px',
                            'width': '300px',
                            'box-sizing': 'border-box',
                        });
                    },
                });
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }
    $scope.getFile = function (input) {
        if (input.files && input.files[0]) {
            $scope.$apply(function () {
                $scope.promoImageFile = input.files[0];
            });
        }
    };

    $scope.submitAddPromo = function () {
        var formData = new FormData();

        var startDateObj = new Date($scope.startDate);
        var endDateObj = new Date($scope.endDate);
        if (isNaN(startDateObj.getTime())) {
            Swal.fire("Error", "Please pick a valid start date.", "error");
            return;
        }
        if (isNaN(endDateObj.getTime())) {
            Swal.fire("Error", "Please pick a valid end date.", "error");
            return;
        }

        formData.append("start_date", startDateObj.toISOString().slice(0, 10));
        formData.append("end_date", endDateObj.toISOString().slice(0, 10));
        formData.append("promoImageFile", $scope.promoImageFile);
        formData.append("PromoName", $scope.promoName);
        formData.append("Description", $scope.promoDescription);

        $http.post("/Home/AddPromo", formData, {
            headers: { "Content-Type": undefined },
            transformRequest: angular.identity
        }).then(function (response) {
            if (response.data.success) {
                Swal.fire("Success", response.data.message, "success").then(function () {
                    window.location.reload();
                });
            } else {
                Swal.fire("Error", response.data.message, "error");
            }
        }).catch(function (error) {
            Swal.fire("Error", "An unexpected error occurred.", "error");
        });
    };

    // EDIT PROMO
    $scope.editPromo = function (p) {
        $scope.editPromoID = p.promoID; 
        $scope.editPromoName = p.name;
        $scope.editpromoDescription = p.description;
        $scope.editstartDate = new Date(p.start_date); 
        $scope.editendDate = new Date(p.end_date);    

        const modalElement = document.getElementById('editModal');
        const modalInstance = M.Modal.init(modalElement);
        modalInstance.open();

        setTimeout(() => {
            M.updateTextFields();
        }, 0);
    };

    $scope.updatePromo = function () {
        var postData = {
            promoID: $scope.editPromoID,
            promoName: $scope.editPromoName,
            description: $scope.editpromoDescription,
            startDate: $scope.editstartDate,
            endDate: $scope.editendDate
        };

        $http.post('/Home/UpdatePromo', postData).then(function (response) {
            if (response.data.success) {
                Swal.fire("Success", "Promo updated successfully!", "success").then(function () {
                    window.location.reload();
                });
            } else {
                Swal.fire("Error", response.data.message, "error");
            }
        }).catch(function (error) {
            Swal.fire("Error", "An unexpected error occurred while updating the promo.", "error");
        });
    };


    // DELETE PROMO
    $scope.deletePromo = function (promoID) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
        }).then((result) => {
            if (result.isConfirmed) {
                LaTableService.deletePromo(promoID).then(function (response) {
                    if (response.data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: response.data.message,
                        });
                        window.location.reload();
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
                        text: 'An error occurred: ' + error.data.message,
                    });
                });
            }
        });
    };

    // LIST BOOKINGS
    $scope.loadBookings = function () {
        LaTableService.getBookings().then(function (response) {
            if (response.data.success) {
                $scope.bookings = response.data.data.map(booking => ({
                    bookingID: booking.ReservationID,
                    userFullName: booking.UserFullName,
                    tableID: booking.TableID,
                    reservationDate: new Date(booking.ReservationDate).toLocaleDateString('en-PH', {
                        month: 'long',
                        day: 'numeric',
                        year: 'numeric'
                    }),
                    reservationTime: booking.ReservationTime,
                    numOfGuests: booking.NumofGuests,
                    statusName: booking.StatusName
                }));
                initializeBookingsTable();
            } else {
                console.error('Failed to load bookings:', response.data.message);
            }
        }, function (error) {
            console.error('Error loading bookings:', error);
        });
    };

    function initializeBookingsTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#bookingsTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 5,
                    language: {
                        search: "Find Booking:",
                        lengthMenu: "",
                    },
                    columns: [
                        { title: "Booking ID" },
                        { title: "User Full Name" },
                        { title: "Table" },
                        { title: "Reservation Date" },
                        { title: "Reservation Time" },
                        { title: "Number of Guests" },
                        { title: "Status" },
                        { title: "Actions", orderable: false },
                    ],
                    initComplete: function () {
                        $('.dataTables_filter').css({
                            'text-align': 'left',
                            'display': 'flex',
                            'align-items': 'center',
                            'gap': '15px',
                            'padding-left': '10px',
                        });

                        $('.dataTables_filter label').css({
                            'font-size': '16px',
                            'color': '#472F35',
                            'font-weight': 'bold',
                            'margin-right': '10px',
                            'margin-bottom': '0',
                        });

                        $('.dataTables_filter input[type="search"]').css({
                            'border': '1px solid #AD9162',
                            'padding': '10px',
                            'background-color': '#F1EDDA',
                            'color': '#472F35',
                            'border-radius': '30px',
                            'width': '300px',
                            'box-sizing': 'border-box',
                        });
                    },
                });
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    // LIST TABLES
    $scope.loadTables = function () {
        LaTableService.getTables().then(function (response) {
            if (response.data.success) {
                $scope.table = response.data.data;
                $scope.$applyAsync(() => {
                    initializeTablesTable();
                });
            } else {
                console.error('Failed to load tables:', response.data.message);
            }
        }, function (error) {
            console.error('Error loading tables:', error);
        });
    };

    function initializeTablesTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#tablesTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 6,
                    language: {
                        search: "Find Table:",
                        lengthMenu: "",
                    },
                    columns: [
                        { title: "Table ID" },
                        { title: "Seating Capacity" },
                        { title: "Actions", orderable: false },
                    ],
                    initComplete: function () {
                        $('.dataTables_filter').css({
                            'text-align': 'left',
                            'display': 'flex',
                            'align-items': 'center',
                            'gap': '15px',
                            'padding-left': '10px',
                        });

                        $('.dataTables_filter label').css({
                            'font-size': '16px',
                            'color': '#472F35',
                            'font-weight': 'bold',
                            'margin-right': '10px',
                            'margin-bottom': '0',
                        });

                        $('.dataTables_filter input[type="search"]').css({
                            'border': '1px solid #AD9162',
                            'padding': '10px',
                            'background-color': '#F1EDDA',
                            'color': '#472F35',
                            'border-radius': '30px',
                            'width': '300px',
                            'box-sizing': 'border-box',
                        });
                    },
                });
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    // ADD TABLE
    $scope.submitAddTable = function () {
        var newTable = {
            SeatingCap: $scope.seatcap,
            createAt: new Date(),
            updateAt: new Date()
        };
        LaTableService.addTable(newTable).then(function (response) {
            if (response.data.success) {
                Swal.fire("Success", "Table Created Successfully!", "success").then(function () {
                    window.location.reload();
                });
            } else {
                Swal.fire("Error", response.data.message, "error");
            }
        }).catch(function (error) {
            Swal.fire("Error", "An unexpected error occurred.", "error");
        });
    };

    // EDIT TABLE
    $scope.editTable = function (t) {
        $scope.editTableID = t.TableID;
        $scope.editSeatcap = t.SeatingCap;
       

        const modalElement = document.getElementById('editModal');
        const modalInstance = M.Modal.init(modalElement);
        modalInstance.open();

        setTimeout(() => {
            M.updateTextFields();
        }, 0);
    }
    $scope.updateTable = function () {
        var editTableID = $scope.editTableID;
        var editSeatcap = $scope.editSeatcap;

        var postData = LaTableService.updateTable(editTableID, editSeatcap);

        window.location.reload();
    }

    // DELETE TABLE
    $scope.deleteTable = function (tableID) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
        }).then((result) => {
            if (result.isConfirmed) {
                LaTableService.deleteTable(tableID).then(function (response) {
                    if (response.data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Deleted!',
                            text: response.data.message,
                        });
                        window.location.reload();
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
                        text: 'An error occurred: ' + error.data.message,
                    });
                });
            }
        });
    };

    // MAIN BOOKING TRIAL

    $scope.reservation = {
        numberOfGuests: null,
        reservationDate: null,
        reservationTime: null
    };

    // RESERVE TABLE
    $scope.reserveTable = function () {
        var reservation = {
            NumofGuests: $scope.reservation.NumofGuests,
            reservationDate: $scope.reservation.reservationDate,
            reservationTime: $scope.reservation.reservationTime
        };

        LaTableService.reserveTable(reservation)
            .then(function (response) {
                if (response.data.success) {
                    Swal.fire("Success", response.data.message, "success")
                        .then(function () {
                            window.location.reload();
                        });
                } else {
                    Swal.fire("Error", response.data.message, "error");
                }
            },
                function (error) {
                    Swal.fire("Error", "There was an error processing your reservation.", "error");
                });
    };

    // LOGOUT
    $scope.logout = function () {
        $http.post('/Home/Logout')
            .then(function (response) {
                if (response.data.success) {
                    $scope.user = null; 

                    window.location.href = "/Home/NoAccountHome"; 
                } else {
                    alert('Logout failed. Please try again.');
                }
            }, function (error) {
                console.log('Error logging out:', error);
                alert('An error occurred while logging out.');
            });
    };

});