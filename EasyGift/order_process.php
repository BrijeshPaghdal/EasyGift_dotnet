<?php
require_once './config.php';
if ($_POST['payment-group'] == 'paytm') {
    $sub_total = $_SESSION['total_price'];
    $_SESSION['fname'] = $_POST['fname'];
    $_SESSION['lname'] = $_POST['lname'];
    $_SESSION['phone'] = $_POST['phone'];
    header("location:checkout_paytm.php?sub_total=$sub_total");
} else {
    session_start();
    $o_id = rand(10000, 99999999);
    $sub_total = $_SESSION['total_price'];
    $user_id = $_SESSION['user_id'];
    $fname = $_POST['fname'];
    $lname = $_POST['lname'];
    $phone = $_POST['phone'];
    if (!empty($_SESSION["shopping_cart"])) {
        foreach ($_SESSION["shopping_cart"] as $keys => $values) {
            $prod_id = $values["product_id"];
            $prod_qty = $values["product_quantity"];
            $total_price  = $values["product_quantity"] * $values["product_price"];
            $date = new DateTime();
            $dateString = $date->format('Y-m-d H:i:s');
            $tempUrl = $url . 'api/EasyGift/Order';
            $data = array('OrderId' => $o_id, 'CustomerId' => $user_id, 'ProductId'=>$prod_id ,'FirstName'=>$fname , 'LastName'=>$lname , 'PhoneNo'=>$phone, 'Quantity'=>$prod_qty, 'TotalPrice'=>$total_price, 'OrderDate'=>$dateString ,'PaymentId'=>1);
            $data = json_encode($data);
            $response = addData($tempUrl,$data);
        }
    }
    unset($_SESSION["shopping_cart"]);
    header("location:index.php");
}
