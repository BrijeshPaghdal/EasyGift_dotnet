<?php
    include 'check-seller.php';
    if(isset($_SESSION['Seller_Name']))
    {
        if(isset($_POST['prod_id']))
        {
            $shop_id = $_SESSION['Shop_Id'];
            $prod_id = $_POST['prod_id'];
            date_default_timezone_set("Asia/Kolkata");

            $tempUrl = $url.'api/EasyGift/Image?filter=ProductId='.$prod_id;
            $res = fetchRequest($tempUrl);
            $image_name = "";
            if ($res['statusCode'] == 200 && count($res['result'])>0) {
              foreach ($res['result'] as $row) {
                  $image_name = $row['imageName'];
                  $filePath = "../product-images/".$image_name;
                    if (file_exists($filePath))
                    {
                        unlink($filePath);
                    }
              }
              $tempUrl = $url.'api/EasyGift/Image?filter=ProductId=='.$prod_id;
              $res = deleteRequest($tempUrl);
            }
            $today = date("y-m-d H:i:s");
            $data = array('ProductStatus'=>2);
            $data = json_encode($data);
            $tempUrl = $url.'api/EasyGift/Product/'.$prod_id;
            $response = patchData($tempUrl,$data);
            if ($response['statusCode']==200 && count($response['result']) > 0) {
                $tempUrl = $url.'api/EasyGift/ProductSuggestion?filter=ProductId=='.$prod_id;
                $res = deleteRequest($tempUrl);
                echo 1;
            }
            else
            {
                echo 2;
            }
        }
    }
?>
