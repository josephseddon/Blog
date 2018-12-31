$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="addEditApplicationRoleModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});

$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="createRoleModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});

$(function () {
	var modalContainer = $('.modal-content');
	$('a[id="deleteApplicationRoleModal"]').click(function (e) {
		e.preventDefault();
		$.get(this.href).done(function (data) {
			modalContainer.html(data);
			modalContainer.find('body').modal('show');
		});
	});
});