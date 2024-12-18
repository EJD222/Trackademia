<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$name = $data->name;
$email = $data->email;
$studentId = $data->studentId;
$address = $data->address;
$birthdate = $data->birthdate;
$program = $data->program;

$sql = "INSERT INTO tbluser (Name, Email, StudentId, Address, Birthdate, Program) VALUES ('$name', '$email', '$studentId', '$address', '$birthdate', $program)";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
