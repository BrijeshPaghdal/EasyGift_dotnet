<?php

$url = "https://localhost:7293/";

	function getLastDataForChart($res,$days)
	{
		$output = "";
		$temp_date = date('Y-m-d', strtotime('-1 day'));
		$cnt = count($res);

		for ($i = 0; $i < $days; $i++) {
			$date = date('Y-m-d', strtotime("-$i days"));
			$found = false;

			foreach ($res as $row) {
				$row_date = date('Y-m-d', strtotime($row['Date']));
				if ($date == $row_date) {
					$output .= $row['Total'];
					$found = true;
					break;
				}
			}

			if (!$found) {
				$output .= '0';
			}

			if ($i < $days-1) {
				$output .= ",";
			}
		}

		return $output;
	}

	function fetchRequest($tempurl){
		$options = array(
			CURLOPT_RETURNTRANSFER => true,												// Return response as a string
			CURLOPT_SSL_VERIFYHOST =>false,
			CURLOPT_SSL_VERIFYPEER => false
		);
	
		return execultAPI($tempurl,$options);
	}

	function deleteRequest($tempurl){
		$options = array(
			CURLOPT_RETURNTRANSFER => true,	
			CURLOPT_CUSTOMREQUEST => 'DELETE',											
			CURLOPT_SSL_VERIFYHOST =>false,
			CURLOPT_SSL_VERIFYPEER => false
		);
	
		return execultAPI($tempurl,$options);
	}
	
	function patchData($patchurl,$data)
	{
		$options = array(
			CURLOPT_RETURNTRANSFER => true,												// Return response as a string
			CURLOPT_CUSTOMREQUEST => 'PATCH',														
			CURLOPT_POSTFIELDS => $data,												// Encode data as JSON and send in the request body
			CURLOPT_HTTPHEADER => array('Content-Type: application/json-patch+json'), 	// Set content type to JSON
			CURLOPT_SSL_VERIFYHOST =>false,
			CURLOPT_SSL_VERIFYPEER => false
		);

		return execultAPI($patchurl,$options);
	}

	function addData($addurl,$data)
	{
		$options = array(
			CURLOPT_RETURNTRANSFER => true,												// Return response as a string
			CURLOPT_CUSTOMREQUEST => 'POST',														
			CURLOPT_POSTFIELDS => $data,												// Encode data as JSON and send in the request body
			CURLOPT_HTTPHEADER => array('Content-Type: application/json-patch+json'), 	// Set content type to JSON
			CURLOPT_SSL_VERIFYHOST =>false,
			CURLOPT_SSL_VERIFYPEER => false
		);

		return execultAPI($addurl,$options);
	}
	function execultAPI($tempurl,$options)
	{
		$ch = curl_init($tempurl);
		curl_setopt_array($ch, $options);
		$response = curl_exec($ch);
		if($err = curl_error($ch))
		{
			return $err;
		}

		curl_close($ch);
		$json = json_decode($response,true);
		// Display the response
		return $json;
	}
	?>