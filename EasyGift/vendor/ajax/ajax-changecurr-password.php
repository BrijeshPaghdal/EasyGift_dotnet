<?php
    include 'check-seller.php';

    if(isset($_POST['passwd']))  
    {
        $currPasswd = $_POST['passwd'];
        $newPasswd = $_POST['newpasswd'];
        $seller_id = $_SESSION['Seller_Id'];

        $tempUrl = $url.'api/EasyGift/Seller/'.$seller_id.'?columns=SellerLoginId';
        $response = fetchRequest($tempUrl);
        if($response['statusCode']==200 && count($response['result'])>0)
        {
            $s_login_id = $response['result']['SellerLoginId'];
        
            $tempUrl = $url.'api/EasyGift/SellerLogin/'.$s_login_id.'?columns=Password';
            $response = fetchRequest($tempUrl);
            if($response['statusCode']==200 && count($response['result'])>0)
            {
                $passwd = $response['result']['Password'];
                $HashPasswd = md5($currPasswd);
                if(strcmp($passwd,$HashPasswd)  == 0 )
                {
                    $newPasswd = md5($newPasswd);
                    $data = array('password' => $newPasswd);
                    $data = json_encode($data);
                    $tempUrl = $url.'api/EasyGift/SellerLogin/'.$s_login_id;
                    $response = patchData($tempUrl,$data);
                    if ($response['statusCode']==200 && count($response['result']) > 0) {
                        setcookie("vendor_login", "", time() - 3600, "/");
                        setcookie("vendor_password", "", time() - 3600, "/");
                        echo "1";
                    }
                }
                else
                {
                    echo "2";
                }
            }
            else
            {
                echo "3";
            }
        }
        else
        {
            echo "3";
        }
    }

?>
