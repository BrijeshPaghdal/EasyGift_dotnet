 <?php
    include 'check-seller.php';

        $seller_id = $_SESSION['Seller_Id'];
        $sub_c_name = $_POST['Sub_C_Name'];
        $prod_name = $_POST['Prod_Name'];
        $comp_name = $_POST['Prod_Company'];
        $price = $_POST['Price'];
        $avai_qua = $_POST['Avail_Qua'];
        $prod_desc = $_POST['Prod_Description'];

        function resize_image($image_name , $max_resolution)
        {
          if(file_exists("../product-images/".$image_name))
          {
            $extension = pathinfo($image_name,PATHINFO_EXTENSION);
            if(strcmp($extension,"png") == 0)
            {
              $original_image = imagecreatefrompng("../product-images/".$image_name);
            }
            else if (strcmp($extension,"jpeg") == 0 || strcmp($extension,"jpg") == 0) {
              $original_image = imagecreatefromjpeg("../product-images/".$image_name);
            }

            // resolution
            $original_width = imagesx($original_image);
            $original_height = imagesy($original_image);

            //try width first
            $ratio = $max_resolution / $original_width;
            $new_width = $max_resolution;
            $new_height = $original_height * $ratio;

            if($new_height > $max_resolution){
              $ratio = $max_resolution / $original_height;
              $new_height = $max_resolution;
              $new_width = $original_width * $ratio;
            }

            if($original_image)
            {
              $new_image = imagecreatetruecolor($new_width,$new_height);

              imagecopyresampled($new_image,$original_image,0,0,0,0,$new_width,$new_height,$original_width,$original_height);
               // imagecopyresized($new_image,$original_image,0,0,0,0,$new_width,$new_height,$original_width,$original_height);
              if(strcmp($extension,"png") == 0)
              {
                imagepng($new_image,"../product-images/".$image_name);
              }
              elseif (strcmp($extension,"jpeg") == 0 || strcmp($extension,"jpg") == 0) {
                imagejpeg($new_image,"../product-images/".$image_name);
              }
            }
            else {
              echo "no";
            }
          }
        }


        if(isset($_FILES['image']) && $_FILES['image']['name'][0] != '' )
        {
            $tempUrl = $url.'api/EasyGift/SubCategory?columns=Id&filter=SubCategoryName.Equals("'.$sub_c_name.'")';
            $res = fetchRequest($tempUrl);
            if ($res['statusCode'] == 200 && count($res['result'])>0) {
            foreach ($res['result'] as $row) {
                $sub_c_id = $row['Id'];
            }
            }

            $shop_id = $_SESSION['Shop_Id'];
            $data = array(  'ShopId' => $shop_id,
                        'SubCategoryId'=>$sub_c_id,
                        'ProductName'=>$prod_name,
                        'CompanyName'=>$comp_name,
                        'Price'=>$price,
                        'AvailableQuantity'=>$avai_qua,
                        'ProductDiscription'=>$prod_desc,
                        'CreatedDate'=>date("Y-m-d H:i:s")
                      );
            $data = json_encode($data);
            $tempUrl = $url.'api/EasyGift/Product';
            $response = addData($tempUrl,$data);
            if ($response['statusCode']==201 && count($response['result']) > 0) {

                $prod_id = $response['result']['id'];

                if(isset($_POST['suggest_for'])){

                    $suggest_for = $_POST['suggest_for'];
                    foreach($suggest_for as $sugg_id) {
                        $data = array(  'ProductId' => $prod_id,
                            'SuggestionId'=>$sugg_id);
                        $data = json_encode($data);
                        $tempUrl = $url.'api/EasyGift/ProductSuggestion';
                        $response = addData($tempUrl,$data);
                    }


                    $temp = 0;
                    foreach($_FILES['image']['name'] as $key=>$val)
                    {
                        $file_name = $_FILES['image']['name'][$key];
                        $file_tmp_name = $_FILES['image']['tmp_name'][$key];
                        $extension = pathinfo($file_name,PATHINFO_EXTENSION);
                        $valid_extension = array("jpg", "jpeg" , "png");

                        if(in_array($extension, $valid_extension))
                        {
                            if( $_FILES['image']['size'][$key]<800000 )
                            {
                                $new_file_name = $_SESSION['Seller_Id']."-".date("d-m-y-h-m-s")."-".rand(1,10000).$file_name;
                                move_uploaded_file($file_tmp_name, "../product-images/".$new_file_name);
                                resize_image($new_file_name , "1000");

                                $data = array('ProductId' => $prod_id,
                                'ImageName'=>$new_file_name);
                                $data = json_encode($data);
                                $tempUrl = $url.'api/EasyGift/Image';
                                $response = addData($tempUrl,$data);
                                if ($response['statusCode']==201 && count($response['result']) > 0) {
                                    $temp = 1;
                                }
                                else
                                {
                                    $temp = 0;
                                    break;
                                }
                            }
                            else
                            {
                                echo "1";
                            }
                        }
                        else
                        {
                            echo "3";
                        }
                    }
                    if($temp == 1)
                    {
                        echo "5";
                    }
                }
                else
                {
                    echo "4";
                }
            }
		}
        else
        {
            echo "2";
        }
?>
