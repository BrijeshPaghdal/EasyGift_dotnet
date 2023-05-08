<?php
    include 'check-seller.php';
    
    $country_name = $_POST['Country'];

    $tempUrl = $url.'api/EasyGift/Countries?filter=CountryName.Equals("'.$country_name.'")';
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $country_id = $response['result'][0]['id'];
    }

    $output = "<select>";

    $tempUrl = $url.'api/EasyGift/States?filter=CountryId='.$country_id;
    $response = fetchRequest($tempUrl);
    if($response['statusCode']==200 && count($response['result'])>0)
    {
        $output .= "<option selected disabled value=''>State</option>";
        foreach($response['result'] as $row)
        {
            $output .= "<option >" . $row['stateName'] . "</option>";
        }
    }
    $output .= "</select>";
    echo $output;
?>