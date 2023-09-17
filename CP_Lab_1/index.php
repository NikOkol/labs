<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="main.css" rel="stylesheet">
    <title>Home</title>
</head>
<body>
    <?php
    session_start();
    require_once('db.php');
    
    if (isset($_SESSION['user_id']))
    {
        echo '<a href="logout.php">Выйти из аккаунта</a><br>';
        $user_id = $_SESSION['user_id'];
        $sql = "SELECT * FROM `users` WHERE id = '$user_id'";
        $result = $conn_users -> query($sql);
        while ($row = mysqli_fetch_array($result))
        {
            $user_name = $row['login'];
        }
        echo '<a href="write_message.php"><button id="write">Написать сообщение</button></a>';
        echo '<div class="user-chats">Сообщения пользователя '. $user_name. '</div>';
        $sql = "SELECT * FROM `messages` WHERE sender = '$user_id' OR destination = '$user_id'";
        $result = $conn_messages -> query($sql);
        

        if ($result->num_rows != 0)
        {
            while ($row = mysqli_fetch_array($result))
            {
                $msg_text = $row['text'];
                $msg_time = $row['timestamp'];
                $sender_id = $row['sender'];
                $dest_id = $row['destination'];
                $sender_name = '';
                $dest_name = '';
                $sql = "SELECT * FROM `users` WHERE id = '$sender_id'";
                $sender_search = $conn_users -> query($sql);
                while ($sender_row = mysqli_fetch_array($sender_search))
                {
                    $sender_name = $sender_row['login'];
                }
                $sql = "SELECT * FROM `users` WHERE id = '$dest_id'";
                $dest_search = $conn_users -> query($sql);
                while ($dest_row = mysqli_fetch_array($dest_search))
                {
                    $dest_name = $dest_row['login'];
                }
                $msg_elems = [ 'From: ' . $sender_name, 'To: ' . $dest_name, $msg_time, $msg_text ];
                echo '<div class="message">';
                $i = 0;
                foreach ($msg_elems as $msg_elem)
                {
                    $class = 'msg-elem';
                    if ($i == 3)
                    {
                        $class = 'msg-text';
                    }
                    echo "<div class=\"$class\">$msg_elem</div>";
                    $i++;
                }
                echo '</div>';
            }
        }
    }
    else
    {
        echo '<a href="registration.php">Регистрация</a><br>';
        echo '<a href="authorization.php">Вход</a>';
    }
    
    ?>
    
    
</body>
</html>
