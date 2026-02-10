class InventoryManager {
    constructor() {
        this.products = [];
    }

    //Add Product
    addProduct(product) {
        const exists = this.products.find(p => p.productID === product.productID);

        if (exists) {
            throw new Error("Product already exists");
        }

        this.products.push({ ...product });
    }

    // Retrive Product
    getProduct(productID) {
        return this.products.find(p => p.productID === productID) || null;
    }

    // Update product
    updateProduct(productID, updates) {
        const product = this.getProduct(productID);

        if (!product) {
            throw new Error("Product not found");
        }

        Object.assign(product, updates);
    }

    //Delete Product
    deleteProduct(productID) {
        const index = this.products.findIndex(p => p.productID === productID);

        if (index === -1) {
            throw new Error("Product not found");
        }

        this.products.splice(index, 1);
    }

    //List ALL
    listProducts() {
        return [...this.products];
    }
}

module.exports = InventoryManager;