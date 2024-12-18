<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Retrieve the student ID from the request
$id = isset($_GET['id']) ? intval($_GET['id']) : 0;

if ($id > 0) {
    $sql = "SELECT tbluser.ID, tbluser.Name, tbluser.Email, tbluser.StudentId, 
                   tbluser.Address, tbluser.Birthdate, tblprogram.ProgramName, tblprogram.ProgramCode
            FROM tbluser 
            LEFT JOIN tblprogram ON tbluser.Program = tblprogram.ID 
            WHERE tbluser.ID = $id";
    
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        $student = $result->fetch_assoc();
        echo json_encode($student);
    } else {
        echo json_encode(["message" => "No student found with the given ID"]);
    }
} else {
    echo json_encode(["message" => "Invalid student ID"]);
}

$conn->close();
?>
