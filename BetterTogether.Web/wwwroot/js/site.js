$(function () {
    var tooltipTriggers = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggers.map(function (trigger) {
        return new bootstrap.Tooltip(trigger)
    });

    const submitSignUpBtn = $('.submit-signup-btn');
    submitSignUpBtn.on('click', (event) => {
        event.preventDefault();

        submitSignUpBtn.prop('disabled', true);
        submitSignUpBtn.find('i').hide();
        submitSignUpBtn.find('.spinner-border').removeClass('d-none');

        submitSignUpBtn.closest('form').submit();
    });

    var eventSelect = $('select[name="SignUpEdit.EventId"]');
    eventSelect.on('change', () => {
        var infoButton = eventSelect.next('button[data-bs-target="#eventDetailsModal"]');
        infoButton.attr('data-event-id', eventSelect.val());
    });

    eventSelect.trigger('change');

    const eventDetailsModal = $('#eventDetailsModal');
    eventDetailsModal.on('show.bs.modal', (event) => {
        var trigger = $(event.relatedTarget);

        $.ajax({
            type: 'GET',
            url: `/SignUps?handler=EventDetails&eventId=${trigger.data('event-id')}`,
            contentType: "application/json",
            dataType: "json",
        }).done(function (event) {
            console.log(event);
            eventDetailsModal.find('#eventName').text(event.name);
            eventDetailsModal.find('#eventDateTime').text(event.startDate);
            eventDetailsModal.find('#eventDescription').html(event.description)
        }).fail(function (error) {
            console.log(error);
        });
    });
});
