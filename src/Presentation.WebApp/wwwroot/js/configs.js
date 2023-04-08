// file of configurations of chart.js graphics
// object for configuration 

const config_ventas = {
  type: 'polarArea',
  data: null,
  options: {}
};
let cvdt = {
  dom:'Brftip',
  buttons:[
    {
      extend:'excelHtml5',
      text:'Excel',
      title:`Ventas recopiladas el ${new Date()}`,
      sheetName:'Ventas',
      autoFiler:true
    },
    {
      extend:'pdfHtml5',
      title:`Ventas recopiladas el ${new Date()}`,
      sheetName:'Ventas',
      text:'PDF',
      autoFiler:true
    },
    'print',
    'colvis'
  ],
  lenghtChange:false,
  scrollX:true,
  searchable:false,
  info:false,
  language:{
    'sProcessing':'Procesando ...',
    'sLengthMenu': 'Mostrar _MENU_ registros',
    'sZeroRecords':'No se encontraron resultados',
    'sEmpityTable':'Ningun dato disponible en esta tabla',
    'sInfo':'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
    'sInfoEmpty':'Mostrando registros del 0 al 0 de un total de 0 registros',
    'sInfoFiltered':'(Filtrado de un total de _MAX_ registros)',
    'sInfoPostFix': "",
    'sSearch':"Buscar:",
    'sUrl':"",
    'sInfoThousands':',',
    'sLoadingRecords':'Cargando ...',
    'oPaginate':{
      'sFirst':'Primero',
      'sLast':'Ultimo',
      'sNext':'Siguiente',
      'sPrevious':'Anterior'
    },
    oAria:{
      'sSortAscending':': Activar para ordenar la columna de manera ascendente',
      'sSortDescending':': Activar para ordenar la columna de mandera descendente'
    }
  }
}
const masterConfigs = {
  'config_ventas':config_ventas,
  'config_ventas_data_table':cvdt
}
const KeysConfigs = Object.keys(masterConfigs);
