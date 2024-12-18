<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Retrieve the student ID from the request
$studentId = isset($_GET['id']) ? intval($_GET['id']) : 0;

if ($studentId > 0) {
    $sql = "SELECT tblacademichistory.ID AS AcademicHistoryID, tblacademichistory.Level, tblacademichistory.SchoolYear, 
                   tblacademichistory.Semester, tblacademichistory.Grade, tblprogram.ProgramName, tbluser.Name AS StudentName
            FROM tblacademichistory
            LEFT JOIN tbluser ON tblacademichistory.StudentID = tbluser.ID
            LEFT JOIN tblprogram ON tblacademichistory.Program = tblprogram.ID
            WHERE tblacademichistory.StudentID = $studentId";

    $result = $conn->query($sql);

    $academicHistories = [];
    while ($row = $result->fetch_assoc()) {
        $academicHistories[] = $row;
    }

    if (!empty($academicHistories)) {
        echo json_encode($academicHistories);
    } else {
        echo json_encode(["message" => "No academic history records found for the given student ID"]);
    }
} else {
    echo json_encode(["message" => "Invalid student ID"]);
}

$conn->close();
?>
