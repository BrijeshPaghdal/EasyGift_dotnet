<?php
    include 'check-admin.php';
    $time = time();
    $time_out_in_sec = 180;
    $time_out = $time - $time_out_in_sec;
    $GetOnlineSellerUrl = $url.'api/EasyGift/SellerOnline?filter=Time>'.$time_out;
    $response = fetchRequest($GetOnlineSellerUrl);
    if($response['statusCode']==200)
    {
        echo count($response['result']);
    }
    elseif($response['statusCode']==404)
    {
        echo 0;
    }
?>
