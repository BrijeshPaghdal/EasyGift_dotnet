<?php
	include 'check-seller.php';
	$shop_id = $_SESSION['Shop_Id'];
  $prod_id = $_POST['prod_id'];
  $tempUrl = $url.'api/EasyGift/Review/GetProductReviews?ProductId='.$prod_id;
  $response = fetchRequest($tempUrl);
  
  if ($response['statusCode']==200 && count($response['result']) > 0) {
      $output = '<div class="product-description">
                    <h2 class="mb-5">Product Review</h2>
                    <table id="quick-product-table" class="table table-hover">';

      foreach ($response['result'] as $row) {
        $cust_name = $row['CustomerName'];
        $prod_name = $row['ProductName'];
        $rating = $row['rating'];
        $review = $row['ReviewDetail'];
        $review_date = $row['ReviewDate'];
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
                    '.date("d-m-Y h:i A",$time).'
                    </span>
                  </h6>';

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
      $output .= '</table></div>';
      echo $output;
    }
    else
    {
      echo "No Record Found";
    }

?>
