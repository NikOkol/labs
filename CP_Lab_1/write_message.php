<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="main.css" rel="stylesheet">
    <title>Write message</title>
</head>
<body>
    <a href="index.php">Назад</a>
    <?php
        session_start();
        if (!isset($_SESSION['user_id']))
        {
            header('Location: index.php');
        }
    ?>
    <div class="form">
    <form action="send_message.php" method="post">

        Введите имя получателя:
        <input type="text" name="dest">
        <?php
            if (isset($_SESSION['dest_not_found']))
            {
                echo '<p>Пользователь не найден</p>';
            }
            if(isset($_SESSION['empty_fields_send']))
            {
                echo '<p>Введены не все значения</p>';
            }
            if(isset($_SESSION['self_send']))
            {
                echo '<p>Нельзя отправить самому себе</p>';
            }
        ?>
        Введите текст:
        <input type="text" name="text">

        <button id="send-btn" type="submit">Отправить</button>

     </form>
    </div>
</body>