<?php
    include 'check-admin.php';
    $time = time();
    $time_out_in_sec = 180;
    $time_out = $time - $time_out_in_sec;

    $GetOnlineCustomerUrl = $url.'api/EasyGift/UserOnline?filter=time>'.$time_out;
    $response = fetchRequest($GetOnlineCustomerUrl);
    if($response['statusCode']==200)
    {
        echo count($response['result']);
    }
    elseif($response['statusCode']==404)
    {
        echo 0;
    }

?>
