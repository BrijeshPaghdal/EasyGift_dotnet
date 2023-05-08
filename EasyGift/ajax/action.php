<?php
include('../config.php');
session_start();

if (isset($_POST["action"])) {
    if ($_POST["action"] == "add") {
        $prod_id = $_POST['product_id'];

        $tempUrl = $url.'api/EasyGift/Product/'.$prod_id;
        $response = fetchRequest($tempUrl);
        if ($response['statusCode']==200 && count($response['result']) > 0) {
          $row = $response['result'];
          $prod_name =  $row['productName'];
          $prod_price =  $row['price'];
        }

        if (isset($_SESSION["shopping_cart"])) {
            $is_available = 0;
            foreach ($_SESSION["shopping_cart"] as $keys => $values) {
                if ($_SESSION["shopping_cart"][$keys]['product_id'] == $_POST["product_id"]) {
                    $is_available++;
                    $_SESSION["shopping_cart"][$keys]['product_quantity'] = $_SESSION["shopping_cart"][$keys]['product_quantity'] + $_POST["product_quantity"];
                }
            }
            if ($is_available == 0) {

                $item_array = array(
                    'product_id'               =>     $_POST["product_id"],
                    'product_name'             =>     $prod_name,
                    'product_price'            =>     $prod_price,
                    'product_quantity'         =>     $_POST["product_quantity"]
                );
                $_SESSION["shopping_cart"][] = $item_array;
            }
        } else {
            $item_array = array(
                'product_id'               =>     $_POST["product_id"],
                'product_name'             =>     $prod_name,
                'product_price'            =>     $prod_price,
                'product_quantity'         =>     $_POST["product_quantity"]
            );
            $_SESSION["shopping_cart"][] = $item_array;
        }
    }
}

if (isset($_POST["action"])) {

    if ($_POST["action"] == 'update') {
        foreach ($_SESSION["shopping_cart"] as $keys => $values) {
            if ($_SESSION["shopping_cart"][$keys]['product_id'] == $_POST["product_id"]) {
                $_SESSION["shopping_cart"][$keys]['product_quantity'] = $_POST["product_quantity"];
            }
        }
    }
}


if (isset($_POST["action"])) {

    if ($_POST["action"] == 'remove') {
        foreach ($_SESSION["shopping_cart"] as $keys => $values) {
            if ($values["product_id"] == $_POST["product_id"]) {
                unset($_SESSION["shopping_cart"][$keys]);
            }
        }
    }
}
