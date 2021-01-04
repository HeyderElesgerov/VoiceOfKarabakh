$('#imageInput').on('change', function () {
	$input = $(this);
	if ($input.val().length > 0) {
		fileReader = new FileReader();
		fileReader.onload = function (data) {
			$('.image-preview').attr('src', data.target.result);
		}
		fileReader.readAsDataURL($input.prop('files')[0]);
		$('.image-button').css('display', 'none');
		$('.image-preview').css('display', 'block');
		$('.change-image').css('display', 'block');
	}
});

$('.change-image').on('click', function () {
	$control = $(this);
	$('#imageInput').val('');
	$preview = $('.image-preview');
	$preview.attr('src', '');
	$preview.css('display', 'none');
	$control.css('display', 'none');
	$('.image-button').css('display', 'block');
});