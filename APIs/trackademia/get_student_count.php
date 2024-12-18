<?php
include 'db.php';

$sql = "SELECT COUNT(*) AS count FROM tbluser";

$result = $conn->query($sql);

if ($result && $row = $result->fetch_assoc()) {
    echo json_encode([
        "success" => true,
        "count" => $row['count']
    ]);
} else {
    echo json_encode([
        "success" => false,
        "message" => "Failed to retrieve user count"
    ]);
}

$conn->close();
?>
