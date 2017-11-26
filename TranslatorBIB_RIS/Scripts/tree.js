(function($)   
{  
    function Tree() {  
        var $this = this;  
  
        
  
        $this.init = function() {  
            treeNodeClick();  
        }  
    }  
    $(function() {  
        var self = new Tree();  
        self.init();  
    })  
}(jQuery))  

