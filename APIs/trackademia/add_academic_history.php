<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$studentId = $data->studentId;
$programId = $data->programId;
$level = $data->level;
$schoolYear = $data->schoolYear;
$semester = $data->semester;
$grade = $data->grade;

$sql = "INSERT INTO tblacademichistory (StudentID, Program, Level, SchoolYear, Semester, Grade) 
        VALUES ($studentId, $programId, '$level', '$schoolYear', '$semester', $grade)";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "Academic history record added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
