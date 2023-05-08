<?php
require_once('config.php');
$o_id = $_GET["oid"];
session_start();
$fname = $_SESSION['fname'];
$lname = $_SESSION['lname'];
$phone = $_SESSION['phone'];
$user_id = $_SESSION['user_id'];
if (!empty($_SESSION["shopping_cart"])) {
    foreach ($_SESSION["shopping_cart"] as $keys => $values) {
        if (!empty($_SESSION["shopping_cart"])) {
            foreach ($_SESSION["shopping_cart"] as $keys => $values) {
                $prod_id = $values["product_id"];
                $prod_qty = $values["product_quantity"];
                $total_price  = $values["product_quantity"] * $values["product_price"];

                $date = new DateTime();
                $dateString = $date->format('Y-m-d H:i:s');
                $tempUrl = $url . 'api/EasyGift/Order';
                $data = array('OrderId' => $o_id, 'CustomerId' => $user_id, 'ProductId'=>$prod_id ,'FirstName'=>$fname , 'LastName'=>$lname , 'PhoneNo'=>$phone, 'Quantity'=>$prod_qty, 'TotalPrice'=>$total_price, 'OrderDate'=>$dateString ,'PaymentId'=>2);
                $data = json_encode($data);
                $response = addData($tempUrl,$data);
            }
        }
    }
    unset($_SESSION["shopping_cart"]);
    unset($_SESSION['fname']);
    unset($_SESSION['phone']);
    unset($_SESSION['lname']);
    header("location: http://localhost/EasyGift/myaccount.php");
}
