var app = angular.module("ABET", ["ngRoute"]);
app.controller('mainCtrl', function ($scope, $http) {
    $scope.nextSemester = 3;
    
    $http({
        method: 'GET',
        url: 'api/Semester/Get'
    }).then(function success(response) {
        $scope.semesters = response.data;
    }, function failure() {

    });


    $scope.selectSemester = function (semester) {
        $scope.addingSemester = false;
        $scope.selectedClass = undefined;
        $scope.selectedSemester = semester;

        $http({
            method: 'GET',
            url: 'api/Class/Get/' + $scope.selectedSemester.Id
        }).then(
            function success(response) {
                $scope.classes = response.data;
            }, function failure() {

            }
        );
    }

    $scope.selectClass = function (cl) {
        $scope.addingClass = false;
        $scope.selectedClass = cl;
    }

    $scope.showNewClass = function () {
        $scope.addingClass = true;
        $scope.selectedClass = new Object();
    }

    $scope.addClass = function () {

        $http({
            method: "POST",
            url: 'api/Class/AddOrUpdate',
            data: {
                Id: $scope.selectedClass.id,
                SemesterId: $scope.selectedSemester.Id,
                CourseName: $scope.selectedClass.CourseName,
                CourseCode: $scope.selectedClass.CourseCode,
                Instructor: $scope.selectedClass.Instructor
            }
        }).then(
            function success(response) {
                var result = response.data;

                $scope.classes.push(result);
            }, function failure() {

            }
        );

        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.selectReview = function (rv) {
        $scope.addingReview = false;
        $scope.selectedReview = rv;
    }

    $scope.addReview = function () {

        $http({
            method: "POST",
            url: 'api/Review/AddOrUpdate',
            data: {
                Id: $scope.selectedReview.id,
                ClassId: $scope.selectedClass.Id,
                Rating: $scope.selectedReview.Rating,
                Text: $scope.selectedReview.Text,
            }
        }).then(
            function success(response) {
                var result = response.data;

                $scope.reviews.push(result);
            }, function failure() {

            }
        );

        $scope.selectedReview = undefined;
        $scope.addingReview = false;
    }

    $scope.showNewReview = function () {
        $scope.selectedReview = undefined;
        $scope.addingReview = true;
    }

    $scope.cancelAddReview = function () {
        $scope.selectedReview = undefined;
        $scope.addingReview = false;
    }

    $scope.cancelAddClass = function () {
        $scope.selectedClass = undefined;
        $scope.addingClass = false;
    }

    $scope.showNewSemester = function () {
        $scope.selectedSemester = undefined;
        $scope.addingSemester = true;
    }
    $scope.addSemester = function () {
        //$scope.selectedSemester.id = $scope.nextSemester;
        //$scope.nextSemester += 1;
        

        $http({
            method: 'POST',
            url: 'api/Semester/AddOrUpdate',
            data: $scope.selectedSemester
        }).then(function success(response) {
            $scope.semesters.push(response.data);
        }, function failure() {

        });

        $scope.selectedSemester = undefined;
        $scope.addingSemester = false;
    }

    $scope.cancelAddSemester = function () {
        $scope.selectedSemester = undefined;
        $scope.addingSemester = false;
    }

    $scope.removeSemester = function (id) {

        $http({
            method: 'GET',
            url: 'api/Semester/RemoveById/' + id
        }).then(function success() {
            var indexToDelete = -1;
            for (i = 0; i < $scope.semesters.length; i++) {
                if ($scope.semesters[i].Id === id) {
                    indexToDelete = i;
                    break;
                }
            }
            if (indexToDelete >= 0) {
                $scope.semesters.splice(indexToDelete, 1);
            }

            if (id === $scope.selectedSemester.Id) {
                $scope.selectedSemester = undefined;
            }
        }, function failure() {

        });
        

    }

    $scope.removeClass = function (id) {

        $http({
            method: 'GET',
            url: 'api/Class/Remove/' + id
        }).then(
            function success(response) {
                var indexToDelete = -1;
                for (i = 0; i < $scope.classes.length; i++) {
                    if ($scope.classes[i].Id === id) {
                        indexToDelete = i;
                        break;
                    }
                }
                if (indexToDelete >= 0) {
                    $scope.classes.splice(indexToDelete, 1);
                }

                if ($scope.selectedClass !== undefined && $scope.selectedClass.Id == id) {
                    $scope.selectedClass = undefined;
                }
            }, function failure() {

            }
        );

        
    }

    $scope.removeReview = function (id) {

        $http({
            method: 'GET',
            url: 'api/Review/Remove/' + id
        }).then(
            function success(response) {
                var indexToDelete = -1;
                for (i = 0; i < $scope.reviews.length; i++) {
                    if ($scope.reviews[i].Id === id) {
                        indexToDelete = i;
                        break;
                    }
                }
                if (indexToDelete >= 0) {
                    $scope.reviews.splice(indexToDelete, 1);
                }

                if ($scope.selectedReview !== undefined && $scope.selectedReview.Id == id) {
                    $scope.selectedReview = undefined;
                }
            }, function failure() {

            }
        );


    }

});
