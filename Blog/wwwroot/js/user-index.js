$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="createEditUserModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});

$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="editUserModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});

$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="deleteUserModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});