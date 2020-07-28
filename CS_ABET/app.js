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

    $http({
        method: 'GET',
        url:'api/Course/Get'
    }).then(function success(response) {
        $scope.courses = response.data;
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
                for (j = 0; j < $scope.classes.length; j++) {
                    for (i = 0; i < $scope.courses.length; i++) {
                        if ($scope.courses[i].Id == $scope.classes[j].CourseId) {
                            $scope.classes[j].Course = $scope.courses[i]
                            break;
                        }
                    }
                }
            }, function failure() {

            }
        );
    }

    //$scope.classes = [{ Id: 0, course: { Id: 0, courseName: 'Software Engineering 1', courseCode: 'CEN 4020' }, semesterId: 2, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 120 },
    //    { Id: 1, course: { Id: 1, courseName: 'C# Application Development', courseCode: 'CIS 4930' }, semesterId: 1, instructor: 'Chris Mills', syllabus: null, canvasLink: '', enrollment: 17 }];

    $scope.selectClass = function (cl) {
        $scope.addingClass = false;
        $scope.selectedClass = cl;
    }

    $scope.showNewClass = function () {
        $scope.addingClass = true;
        $scope.selectedClass = new Object();
        $scope.selectedClass.course = new Object();
    }

    $scope.addClass = function () {

        $http({
            method: "POST",
            url: 'api/Class/AddOrUpdate',
            data: {
                Id: $scope.selectedClass.id,
                SemesterId: $scope.selectedSemester.Id,
                CourseId: $scope.selectedClass.Course.Id,
                Instructor: $scope.selectedClass.Instructor,
                Enrollment: $scope.selectedClass.Enrollment
            }
        }).then(
            function success(response) {
                var result = response.data;
                for (i = 0; i < $scope.courses.length; i++) {
                    if ($scope.courses[i].Id == result.CourseId) {
                        result.Course = $scope.courses[i]
                        break;
                    }
                }
                $scope.classes.push(result);
            }, function failure() {

            }
        );

        $scope.selectedClass = undefined;
        $scope.addingClass = false;
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
});
