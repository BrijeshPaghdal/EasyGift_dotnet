<?php
  require_once('./check-admin.php');
  function deleteCate($url,$cate_id)
  {

    $tempUrl = $url.'api/EasyGift/Category?columns=CategoryImageName&filter=Id='.$cate_id;
    $response = fetchRequest($tempUrl);

    if ($response['statusCode'] == 200 && count($response['result'])>0) 
    {
        foreach ($response['result'] as $row) {
            $image_name = $row['CategoryImageName'];

            $filePath = "../category-image/" . $image_name;
            if (file_exists($filePath)) {
                unlink($filePath);
            }else {
                echo "2";
            }
        }
        $tempUrl = $url.'api/EasyGift/Category/'.$cate_id;
        $response = deleteRequest($tempUrl);
        if ($response['statusCode'] == 200) {
            echo "1";
        } else {
            echo "3";
        }
    }
  }

    if(isset($_POST['cate_id']))
    {
      $cate_id = $_POST['cate_id'];

      $tempUrl = $url.'api/EasyGift/SubCategory?columns=Id&filter=CategoryId='.$cate_id;
      $response = fetchRequest($tempUrl);
      if($response['statusCode']==404)
      {
          deleteCate($url,$cate_id);
      }
      else
      {
        $temp = 0;
        if (count($response['result']) > 0) {
          foreach ($response['result'] as $row1) {
              $sub_c_id = $row1['Id'];
              
              $tempUrl = $url.'api/EasyGift/Product?columns=Id&columns=ProductStatus&filter=SubCategoryId='.$sub_c_id;
              $response2 = fetchRequest($tempUrl);

              if ($response2['statusCode'] == 200 && count($response2['result'])>0) {

                foreach($response2['result'] as $row2) {
                    $prod_id = $row2['Id'];
                    $p_status =0;
                    $s_c_id = 0;

                    $data = array('ProductStatus' => $p_status,'SubCategoryId'=>$s_c_id);
                    $data = json_encode($data);
                    $tempUrl = $url . 'api/EasyGift/Product/'.$prod_id;
                    $response3 = patchData($tempUrl,$data);
                    if ($response3['statusCode'] == 200) {
                      $temp = 1;
                    } elseif ($response3['statusCode'] == 404) {
                      $temp =0;
                      echo "2";
                    }
                }
              }
              else {
                $temp = 1;
              }
          }
        }
        if($temp == 1)
        {
          deleteCate($url,$cate_id);
        }
      }

    }
?>
