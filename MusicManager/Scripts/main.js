$('.owl-carousel').owlCarousel({
	loop: true,
	dots: false,
	margin: 10,
	responsiveClass: true,
	responsive: {
		0: {
			items: 1,
			nav: false,
			dots: true
		},
		600: {
			items: 2,
			nav: true
		},
		1000: {
			items: 3,
			nav: true,
			loop: false
		}
	}
})

$(document.body).on('click touchstart', '.js-album-details', getAlbumDetails);

function getAlbumDetails() {
	var $this = $(this);

	var albumId = $this.attr('data-id');

	$.ajax({
		url: '/Library/JsonAlbumDetails',
		type: 'GET',
		dataType: 'json',
		data: 'albumId=' + albumId
	})
		.done(function (data) {
			var id = "#" + albumId;
			$(id).html(data.message);
	});


}