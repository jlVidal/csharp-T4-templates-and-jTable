	
var JTableExtension = function()
{
	var self = this;

	self.getDataFields = function()
	{
		return {
  "Id": {
    "title": "Id",
    "type": "text",
    "key": true,
    "edit": true,
    "create": true
  },
  "LastUpdate": {
    "title": "Last Update",
    "type": "date",
    "inputClass": "validate[required,custom[date]]"
  },
  "Active": {
    "title": "Active",
    "type": "checkbox",
    "values": {
      "false": "No",
      "true": "Yes"
    },
    "defaultValue": "false"
  },
  "OtherFlag": {
    "title": "Other Flag",
    "type": "checkbox",
    "values": {
      "false": "No",
      "true": "Yes"
    },
    "defaultValue": "false"
  },
  "FKFirstNotNull": {
    "title": "FK - First Not Null",
    "type": "text",
    "inputClass": "validate[required,custom[integer]]"
  },
  "FKSecondNull": {
    "title": "FK - Second Null",
    "type": "text",
    "inputClass": "validate[custom[number]]"
  },
  "FKThirdDefaultValue": {
    "title": "FK - Third Default Value",
    "type": "text",
    "inputClass": "validate[custom[number]]"
  },
  "Labels": {
    "title": "Labels",
    "type": "text"
  }
};
	}

	self.connect = function(element)
	{
		if (!(element instanceof jQuery))
			element = $(element);

		 var fieldsConfig = self.getDataFields();

		 element.jtable({
            tableId: 'FooTable',
            title: 'Foo List',
            paging: true,
            sorting: true,
            //columnResizable: true, //Actually, no need to set true since it's default
            //columnSelectable: true, //Actually, no need to set true since it's default
            //saveUserPreferences: true, //Actually, no need to set true since it's default
            //defaultSorting: 'Name ASC',
            actions: {
                listAction: '/Foo/ListAll',
                deleteAction: '/Foo/Delete',
                updateAction: '/Foo/Update',
                createAction: '/Foo/Create'
            },
			fields: fieldsConfig,
						formCreated: function (event, data) {
                data.form.validationEngine();
				$(data.form).addClass("custom_horizontal_form_field");

				var allKeys = Object.keys(fieldsConfig);
				for (var i = 0; i < allKeys.length; i++) {
					var field = allKeys[i];
					if (fieldsConfig[field].key)
					{
						var element = $(data.form).find("[name='" + field + "']");
						if (data.formType === 'edit')
						{
							element.attr('readonly','true')
						}
						else
						{
							element.attr('disabled','disabled')
						}
						
					}
				}
            },
            //Validate form when it is being submitted
            formSubmitting: function (event, data) {
                return data.form.validationEngine('validate');
            },
            //Dispose validation logic when form is closed
            formClosed: function (event, data) {
                data.form.validationEngine('hide');
                data.form.validationEngine('detach');
            } 
		  });
 
        //Load stuff from server
        element.jtable('load');
	
	}
}
