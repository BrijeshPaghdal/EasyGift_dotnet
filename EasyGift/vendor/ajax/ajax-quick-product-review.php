<?php
	include 'check-seller.php';
	$shop_id = $_SESSION['Shop_Id'];
  $tempUrl = $url.'api/EasyGift/Review/GetProductReviews?ShopId='.$shop_id;
  $response = fetchRequest($tempUrl);
  $output = "";

  if($response['statusCode']==200 && count($response['result'])>0)
  {
      $output = '<table id="quick-product-table" class="table table-hover">';

      foreach ($response['result'] as  $row) {
        $cust_name = $row['CustomerName'];
        $prod_name=$row['ProductName'];
        $rating=$row['rating'];
        $review=$row['ReviewDetail'];
        $review_date=$row['ReviewDate'];
        $review_date = new DateTime($review_date);
        $review_date = $review_date->format('d-m-Y');

        $time = strtotime($review_date);

        $output .= '<tr><td><div class="row">
                <div class="review-img">
                  <i class="material-icons col-green" style= "font-size:35px">account_circle</i>
                </div>
                <div class="col">
                  <h6 class="m-b-15">
                    '.$cust_name.'
                    <span class="float-right m-r-10 text-muted">
                    '.date("d-m-Y",$time).'
                    </span>
                  </h6>

                  <h5>'.$prod_name.'</h5>';

                  for ($i=0; $i < 5 ; $i++) {
                    if($i < $rating)
                    {
                      $output .= '<i class="material-icons col-orange">star</i>';
                    }
                    else
                    {
                      $output .= '<i class="material-icons col-orange">star_border</i>';
                    }
                  }


          $output .='<p class="m-t-15 m-b-15">
                    '.$review.'
                  </p>
                </div>
              </div></td></tr>';

      }
      $output .= '</table>';
      echo $output;
    }
		else {
			{
				echo "no review found...";
			}
		}

?>
