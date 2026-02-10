class ShoppingCart {
    constructor() {
        this.items = [];
        this.discountPercent = 0;
    }

    //US-SC-01 Add Item
    addItem(name, price, quantity) {
        if (!name || price <=0 || quantity <=0) return;

        this.items.push({
            name,
            price,
            quantity
        });
    }

    //US-SC-02 Remove Items(s)
    removeItems(name) {
        this.items = this.items.filter(item => item.name !== name);
    }

    //US-SC-03 Total Price 
    getTotal() {
        return this.items.reduce((sum, item) => {
            return sum + (item.price * item.quantity);
        }, 0);
    }

    //US-SC-04 Discount
    applyDiscount(code) {
        const map = {
            SAVE10: 10,
            SAVE20: 20,
            SAVE30: 30,
        };
        this.discountPercent = map[code] || 0;
    }

    //US-SC-05 Tax (before discount)
    calculateTax(rate) {
        const subtotal = this.getTotal();
        return subtotal * rate;
    }

    // US-SC-06 Checkout 
    checkout(taxRate = 0) {
        const subtotal = this.getTotal();
        const tax = subtotal * taxRate;

        const TotalBeforeDiscount = subtotal + tax;

        const discountAmount = (TotalBeforeDiscount * this.discountPercent) / 100;

        const finalAmount = TotalBeforeDiscount - discountAmount;

        this.items = []; // clear cart
        this.discountPercent = 0;

        return Number(finalAmount.toFixed(2));
    }
}

module.exports = ShoppingCart;












