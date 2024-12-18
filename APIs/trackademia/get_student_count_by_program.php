<?php
include 'db.php';

$sql = "SELECT tblprogram.ProgramName, COUNT(tbluser.ID) AS StudentCount 
        FROM tblprogram
        LEFT JOIN tbluser ON tblprogram.ID = tbluser.Program 
        GROUP BY tblprogram.ProgramName";

error_log("Executing SQL: $sql"); // Log SQL query

$result = $conn->query($sql);

if ($result) {
    $data = [];
    while ($row = $result->fetch_assoc()) {
        $data[] = [
            "ProgramName" => $row['ProgramName'],
            "StudentCount" => (int) $row['StudentCount']
        ];
        error_log("Fetched row: " . json_encode($row)); // Log each row
    }
    echo json_encode([
        "success" => true,
        "data" => $data
    ]);
    error_log("Response sent: " . json_encode($data)); // Log final response
} else {
    error_log("SQL error: " . $conn->error); // Log SQL error
    echo json_encode([
        "success" => false,
        "message" => "Failed to retrieve data"
    ]);
}

$conn->close();
?>
