mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  comunicarCodigoAtividade: function(valor){
    window.parent.postMessage({cmd: "tarefaCodigo",params: {'valor':Pointer_stringify(valor)}},"*");
  },

  comunicarFimAtividade: function(){
    window.parent.postMessage("atividadeFinalizada","*");
  }
});