const ShoppingCart = require('./shoppingCart');
const InventoryManager = require('./inventoryManager');

console.log("=== CART TEST ===");
const cart = new ShoppingCart();

cart.addItem("pen", 10, 2);
cart.addItem("Book", 50, 1);

cart.applyDiscount("SAVE10");

const final = cart.checkout(0.1);
console.log("Final Amount:" , final);

console.log("=== INVENTORY TEST ===");
const inv = new InventoryManager();

inv.addProduct({ productID: 1, name: "Laptop", category: "Tech", price: 50000, stock: 5});

console.log(inv.getProduct(1));

inv.updateProduct(1, { stock: 10 });

console.log(inv.listProducts());