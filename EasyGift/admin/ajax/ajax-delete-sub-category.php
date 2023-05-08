<?php
  include 'check-admin.php';
  function deleteSubCate($url,$sub_c_id)
  {
    $tempUrl = $url.'api/EasyGift/SubCategory/'.$sub_c_id;
    $response = deleteRequest($tempUrl);
    if ($response['statusCode'] == 200) {
        echo "1";
    } else {
        echo "3";
    }
  }

    if(isset($_POST['sub_c_id']))
    {
      $sub_c_id = $_POST['sub_c_id'];

        $temp = 0;
        $tempUrl = $url.'api/EasyGift/Product?columns=Id&filter=SubCategoryId='.$sub_c_id;
        $response = fetchRequest($tempUrl);
        if($response['statusCode']==404)
        {
          deleteSubCate($url,$sub_c_id);
        }
        else
        {
          
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
        if($temp == 1)
        {
          deleteSubCate($url,$sub_c_id);
        }
    }
    else {
      echo "2";
    }
?>
