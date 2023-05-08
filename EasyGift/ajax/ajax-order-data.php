<?php
session_start();
require_once('../config.php');
$user_id = $_SESSION['user_id'];
$tempUrl = $url.'api/EasyGift/Customer/'.$user_id.'/GetOrders';

$response = fetchRequest($tempUrl);
if ($response['statusCode'] == 200 && count($response['result'])>0) {

    $output = '<table class="table table-wishlist table-mobile" id="order-details">
    <thead>
        <tr>
            <th>Product Name</th>
            <th>Shop Name</th>
            <th>Order Quantity</th>
            <th>total Price</th>
            <th>Order Date</th>
        </tr>
    </thead>';

    foreach ($response['result'] as $row) {
        $prod_id = $row['ProductId'];
        $prod_name = $row['ProductName'];
        $prod_quntity = $row['Quantity'];
        $prod_total_price = $row['TotalPrice'];
        $order_date = $row['OrderDate'];
        $shop_name = $row['ShopName'];
        $o_d = explode(" ", $order_date);


        $output .= "

        <tbody>
            <tr>
                <td class='product-col'>
                    <div class='product'>
                        <h3 class='product-title'>
                            <a href=\"product.php?prod_id={$prod_id}\">{$prod_name}</a>
                        </h3>
                    </div>
                </td>
                <td class='product-col'>
                <div class='product'>
                    <h3 class='product-title'>
                        <a>{$shop_name}</a>
                    </h3>
                </div>
                </td>
                <td class='product-col'>
                <div class='product'>
                    <h3 class='product-title'>
                        <a>{$prod_quntity}</a>
                    </h3>
                </div>
                </td>
                
                <td class='price-col'>{$prod_total_price}</td>
                <td class='product-col'>
                <div class='product'>
                    <h3 class='product-title'>
                        <a>{$o_d[0]}</a>
                    </h3>
                </div>
                </td>

            </tr>

        </tbody>
        </table>";
    }
    echo $output;
} else {
    echo "<p>No orders available yet.</p>";
}
