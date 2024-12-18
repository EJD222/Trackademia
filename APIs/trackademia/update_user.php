<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$id = $data->id;
$name = $data->name;
$email = $data->email;
$studentId = $data->studentId;
$address = $data->address;
$birthdate = $data->birthdate;
$program = $data->program;

$sql = "UPDATE tbluser 
        SET Name='$name', Email='$email', StudentId='$studentId', Address='$address', Birthdate='$birthdate', Program=$program 
        WHERE ID=$id";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User updated successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
