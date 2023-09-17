<?php
session_start();
require_once('db.php');
$dest_name = $_POST['dest'];
$text = $_POST['text'];
$sender = $_SESSION['user_id'];
unset($_SESSION['dest_not_found']);
unset($_SESSION['empty_fields_send']);
unset($_SESSION['self_send']);
function redirect($location)
{
    header('Location: '. $location);
    exit;
}
$sql = "SELECT * FROM `users` WHERE login = '$dest_name'";
$result = $conn_users->query($sql);
if ($result->num_rows == 0)
{
    $_SESSION['dest_not_found'] = 1;
    redirect('write_message.php');
}

if (empty($dest_name) || empty($text))
{
    $_SESSION['empty_fields_send'] = 1;
    redirect('write_message.php');
}

while ($row = mysqli_fetch_array($result))
{
    if ($row['id'] == $_SESSION['user_id'])
    {
        $_SESSION['self_send'] = 1;
        redirect('write_message.php');
    }
    $dest_id = $row['id'];
    $sql = "INSERT INTO `messages` (sender, destination, text) VALUES ('$sender', '$dest_id', '$text')";
    $conn_messages -> query($sql);
    redirect('index.php');
}


?>